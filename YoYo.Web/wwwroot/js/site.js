// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
jQuery(function ($) {

    let level = 1;
    let shuttle = 1;
    let speed = 8;
    let _max = 120;
    let _goal = 120;
    let baseAPIUri = "https://localhost:44337/api/";

    let cofitnessRatingBeepcommulativeTime = "00:00";
    // $('.pie_progress--countdown').asPieProgress({
    //namespace: 'pie_progress',
    //easing: 'linear',
    //first: 0,
    //max: _max,
    //goal: _goal,
    //speed: (_max * 1000) / 100,// 1200, // 120 s * 1000 ms per s / 100
    //numberCallback: function (n) {
    //    var minutes = Math.floor(this.now / 60);
    //    var seconds = this.now % 60;
    //    if (seconds < 10) {
    //        seconds = '0' + seconds;
    //    }
    //    if (initalTime == seconds) {
    //        initalTime += 5;
    //        level++;
    //        shuttle++;
    //        speed += 4;
    //    }


    //    let levelDisplay = `<span>Level ${level}</span><br />
    //                                <span>Shuttle ${shuttle}</span><br />
    //                                <span>${speed} km/h</span>`;

    //    $("#spnTotalTime").text(minutes + ': ' + seconds + 'm');
    //    return levelDisplay;//minutes + ': ' + seconds;
    //}
    //});

    $('#button_start').on('click', function () {
        postData("Home/StartTest", {}, function (result) {
       // postData("https://localhost:44337/api/Fitness", {}, function (result) {
            debugger;
            console.log(result);
            //$('.pie_progress--countdown').asPieProgress();
            if (result != null) {
                let accumulatedShuttleDistance = Math.max.apply(Math, result.map(function (o) { return (o.accumulatedShuttleDistance); }));
                var commulativeTime = result.find(item => item.accumulatedShuttleDistance == accumulatedShuttleDistance).commulativeTime;
                var time = commulativeTime.split(':'); // split it at the colons

                if (time.length > 2) {
                    _max = (+time[0]) * 60 * 60 + (+time[1]) * 60 + (+time[2]);
                    _goal = (+time[0]) * 60 * 60 + (+time[1]) * 60 + (+time[2]);
                } else {
                    _max = (+00) * 60 * 60 + (+time[0]) * 60 + (+time[1]);
                    _goal = (+00) * 60 * 60 + (+time[0]) * 60 + (+time[1]);
                }
                $('.pie_progress--countdown').asPieProgress({
                    namespace: 'pie_progress',
                    easing: 'linear',
                    first: 0,
                    max: _max,
                    goal: _goal,
                    speed: (_max * 1000) / 100,// 1200, // 120 s * 1000 ms per s / 100
                    numberCallback: function (n) {
                        var minutes = Math.floor(this.now / 60);
                        var seconds = this.now % 60;
                        if (seconds < 10) {
                            seconds = '0' + seconds;
                        }
                        if (minutes < 10) {
                            minutes = '0' + minutes;
                        }
                        //if (initalTime == seconds) {
                        //    initalTime += 5;
                        //    level++;
                        //    shuttle++;
                        //    speed += 4;
                        //}
                        

                        
                        let currentTime = minutes + ':' + seconds;
                        let fitnessRatingBeep = result.find(item => item.startTime == currentTime);

                        if (typeof (fitnessRatingBeep) != "undefined") {
                            level = fitnessRatingBeep.speedLevel;
                            shuttle = fitnessRatingBeep.shuttleNo;
                            speed = fitnessRatingBeep.speed;


                            //let nextShuttleCommulativeTime = fitnessRatingBeep.commulativeTime.split(':');
                            //let nextShuttleStartTime = fitnessRatingBeep.startTime.split(':');
                            
                            cofitnessRatingBeepcommulativeTime = fitnessRatingBeep.commulativeTime;
                            //StartTime
                        }

                        let ddif = diff(currentTime, cofitnessRatingBeepcommulativeTime);
                        $("#spnNextShuttle").text(ddif+ " m");
                        {
                            
                            let totalSec = parseInt(minutes) * 60 + parseInt( seconds);
                            var hours = (totalSec / 60) / 60;    
                            let distance = (parseFloat(speed) * hours) * 1000; // distance in meter
                            $("#spnTotalDistance").text(distance.toFixed(2) + " m");
                        }
                        let levelDisplay = `<span>Level ${level}</span><br />
                                        <span>Shuttle ${shuttle}</span><br />
                                        <span>${speed} km/h</span>`;

                        $("#spnTotalTime").text(minutes + ':' + seconds );
                        return levelDisplay;//minutes + ': ' + seconds;
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

    $(".warn-athletes").on("click", function () {
        let _this = this;
        let athleteId = $(_this).data("id");
        postData("Home/WarnAthletes/" + athleteId, {}, function (result) {
            if (result == true) {
                $(_this).attr("disabled", true).addClass("warned").text("Warned");
            }
        }, "Get");

    });

    $(".stop-athletes").on("click", function () {
        let _this = this;
        let athleteId = $(_this).data("id");
        let stopTime = $("#spnTotalTime").text();
        postData("Home/StopAthletes?id=" + athleteId + "&stopTime=" + stopTime, {}, function (result) {
            if (result == true) {
                $(_this).addClass("hide");
                $(_this).parent(".col-md-3").find(".result-athletes").text(stopTime);
                $(_this).parents(".form-signin .row").find(".warn-athletes").addClass("hide");

                if ($(".stop-athletes.hide").length == $(".stop-athletes").length) {
                    $('.pie_progress').asPieProgress('finish');
                }
            }
        }, "Get");
    });

    $('#button_stop').on('click', function () {
        $('.pie_progress').asPieProgress('stop');
    });

    $('#button_finish').on('click', function () {
        $('.pie_progress').asPieProgress('finish');
    });

    

    $('#button_reset').on('click', function () {
        $('.pie_progress').asPieProgress('reset');
    });

   

   

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

    function diff(start, end) {
        start = start.split(":");
        end = end.split(":");
        var startDate = new Date(0, 0, 0, start[0], start[1], 0);
        var endDate = new Date(0, 0, 0, end[0], end[1], 0);
        var diff = endDate.getTime() - startDate.getTime();
        var hours = Math.floor(diff / 1000 / 60 / 60);
        diff -= hours * 1000 * 60 * 60;
        var minutes = Math.floor(diff / 1000 / 60);

        // If using time pickers with 24 hours format, add the below line get exact hours
        if (hours < 0)
            hours = hours + 24;

        return (hours <= 9 ? "0" : "") + hours + ":" + (minutes <= 9 ? "0" : "") + minutes;
    }
});
