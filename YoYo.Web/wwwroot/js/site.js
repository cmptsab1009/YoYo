jQuery(function ($) {

    fillTimeDropdown();

    let level = 1;
    let shuttle = 1;
    let speed = 8;
    let _max = 120;
    let _goal = 120;
    let cofitnessRatingBeepcommulativeTime = "00:00";

    //click event to start the test
    $('#button_start').on('click', function () {
        postData("Home/StartTest", {}, function (result) {
            if (result != null) {
                let accumulatedShuttleDistance = Math.max.apply(Math, result.map(function (o) { return (o.accumulatedShuttleDistance); }));
                let commulativeTime = result.find(item => item.accumulatedShuttleDistance == accumulatedShuttleDistance).commulativeTime;
                let time = commulativeTime.split(':'); // split it at the colons

                if (time.length > 2) {
                    _max = (+time[0]) * 60 * 60 + (+time[1]) * 60 + (+time[2]);
                    _goal = (+time[0]) * 60 * 60 + (+time[1]) * 60 + (+time[2]);
                } else {
                    _max = (+00) * 60 * 60 + (+time[0]) * 60 + (+time[1]);
                    _goal = (+00) * 60 * 60 + (+time[0]) * 60 + (+time[1]);
                }

                //start progress bar and display shuttle, speed, distance infotmation.
                $('.pie_progress--countdown').asPieProgress({
                    namespace: 'pie_progress',
                    easing: 'linear',
                    first: 0,
                    max: _max,
                    goal: _goal,
                    speed: (_max * 1000) / 100,// 1200, // 120 s * 1000 ms per s / 100
                    numberCallback: function (n) {
                        let minutes = Math.floor(this.now / 60);
                        let seconds = this.now % 60;
                        if (seconds < 10) {
                            seconds = '0' + seconds;
                        }
                        if (minutes < 10) {
                            minutes = '0' + minutes;
                        }

                        let currentTime = minutes + ':' + seconds;
                        let fitnessRatingBeep = result.find(item => item.startTime == currentTime);

                        if (typeof (fitnessRatingBeep) != "undefined") {
                            level = fitnessRatingBeep.speedLevel;
                            shuttle = fitnessRatingBeep.shuttleNo;
                            speed = fitnessRatingBeep.speed;

                            cofitnessRatingBeepcommulativeTime = fitnessRatingBeep.commulativeTime;
                        }

                        $("#spnNextShuttle").text(timeDifferenceCalculator(currentTime, cofitnessRatingBeepcommulativeTime) + " m");
                        {
                            let totalSecond = parseInt(minutes) * 60 + parseInt(seconds);
                            let hours = (totalSecond / 60) / 60;
                            let distance = (parseFloat(speed) * hours) * 1000; //convert distance in meter
                            $("#spnTotalDistance").text(distance.toFixed(2) + " m");
                        }

                        $("#spnTotalTime").text(minutes + ':' + seconds);

                        return `<span>Level ${level}</span><br />
                                <span>Shuttle ${shuttle}</span><br />
                                <span>${speed} km/h</span>`;
                    }
                });

                $(".stop-athletes").removeClass("hide");
                $(".warn-athletes").removeClass("hide");

                $(".start-test").addClass("hide");
                $('.pie_progress').asPieProgress('start');
                $(".runnig-test").removeClass("hide");
            }
        }, "Get");
    });

    //click event to warn athletes
    $(".warn-athletes").on("click", function () {
        let _this = this;
        let athleteId = $(_this).data("id");
        postData("Home/WarnAthletes/" + athleteId, {}, function (result) {
            if (result == true) {
                $(_this).attr("disabled", true).addClass("warned").text("Warned");
            }
        }, "Get");
    });

    //click event to stop athletes
    $(".stop-athletes").on("click", function () {
        let _this = this;
        let athleteId = $(_this).data("id");
        let stopTime = $("#spnTotalTime").text();
        postData("Home/StopAthletes?id=" + athleteId + "&stopTime=" + stopTime, {}, function (result) {
            if (result == true) {
                $(_this).addClass("hide");
                $(_this).parent(".col-md-3").find(".result-panel").removeClass("hide");
                $(_this).parent(".col-md-3").find(".result-athletes").text(stopTime);
                $(_this).parents(".form-signin .row").find(".warn-athletes").addClass("hide");

                if ($(".stop-athletes.hide").length == $(".stop-athletes").length) {

                    let totalDistance = $("#spnTotalDistance").text();
                    $('.pie_progress').asPieProgress('finish');

                    $("#spnTotalTime").text(stopTime);
                    $("#spnNextShuttle").text("0:00 m");
                    $("#spnTotalDistance").text(totalDistance);
                }
            }
        }, "Get");
    });

    //click event to display time updation
    $(".result-panel").on("click", function () {
        debugger;
        let _this = this;
        let athleteId = $(_this).data("id");
        debugger;

        let currentTime = $(_this).find(".result-athletes").text();
        let mmss = currentTime.split(":");

        $(_this).parents(".form-signin .row").find(".update-athletes-time .minutes").val(mmss[0]);
        $(_this).parents(".form-signin .row").find(".update-athletes-time .second").val(mmss[1]);
        $(_this).parents(".form-signin .row").find(".update-athletes-time").removeClass("hide");
        $(_this).addClass("hide");
    });

    //click event to update time of athletes
    $(".fa-check").on("click", function () {
        let _this = this;
        let athleteId = $(_this).data("id");

        let mm = $(_this).parents(".form-signin .row").find(".update-athletes-time .minutes").val();
        let ss = $(_this).parents(".form-signin .row").find(".update-athletes-time .second").val();
        let stopTime = mm + ":" + ss;

        postData("Home/StopAthletes?id=" + athleteId + "&stopTime=" + stopTime, {}, function (result) {
            if (result == true) {
                debugger;
                $(".form-signin").find(`.row[data-athlet-id='${athleteId}']`).find(".result-panel").find(".result-athletes").text(stopTime);
                $(".form-signin").find(`.row[data-athlet-id='${athleteId}']`).find(".result-panel").removeClass("hide");
                $(".form-signin").find(`.row[data-athlet-id='${athleteId}']`).find(".update-athletes-time").addClass("hide");
            }
        }, "Get");

    });

    //click enent to cancle updation
    $(".fa-times").on("click", function () {
        let _this = this;
        let athleteId = $(_this).data("id");
        //$(".form-signin").find(`.row[data-athlet-id='${athleteId}']`).find(".result-panel").find(".result-athletes").text(stopTime);
        $(".form-signin").find(`.row[data-athlet-id='${athleteId}']`).find(".result-panel").removeClass("hide");
        $(".form-signin").find(`.row[data-athlet-id='${athleteId}']`).find(".update-athletes-time").addClass("hide");
    });

    //common method to call api
    postData = function (url, data, callback, methodType) {
        methodType = methodType || "POST";
        $.support.cors = true;
        $.ajax({
            url: url,
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS',
                'Access-Control-Allow-Headers': 'Origin, Content-Type, X-Auth-Token'
            },
            contentType: 'application/json',
            data: JSON.stringify(data),
            type: methodType,
            dataType: 'json',
            crossDomain: true,
            success: function (result, textStatus, xhr) {
                callback(result);
            },
            error: function (xhr, status, error) {
                console.log(xhr, status, error);
            },
        });
    };

    //calculate time difference between two time
    function timeDifferenceCalculator(start, end) {
        start = start.split(":");
        end = end.split(":");
        let startDate = new Date(0, 0, 0, start[0], start[1], 0);
        let endDate = new Date(0, 0, 0, end[0], end[1], 0);
        let diff = endDate.getTime() - startDate.getTime();
        let hours = Math.floor(diff / 1000 / 60 / 60);
        diff -= hours * 1000 * 60 * 60;
        let minutes = Math.floor(diff / 1000 / 60);

        // If using time pickers with 24 hours format, add the below line get exact hours
        if (hours < 0)
            hours = hours + 24;

        return (hours <= 9 ? "0" : "") + hours + ":" + (minutes <= 9 ? "0" : "") + minutes;
    }

    //fill dropdown for minuts and second
    function fillTimeDropdown() {
        for (var time = 0; time < 60; time++) {
            if (time < 10) {
                $('.update-athletes-time select').append($('<option>').text('0' + time).attr('value', '0' + time));
            } else {
                $('.update-athletes-time select').append($('<option>').text(time).attr('value', time));
            }
        }
    }
});
