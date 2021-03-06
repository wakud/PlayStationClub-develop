// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*
	Система загружает выпадающий список с доступным временем начала сеанса исходя из ранее указанных Пользователем данных:
	Дата заказа и длительность, а также график работы клуба.
*/
//Относительно графика работы клуба выставляем 
//Минимальное время работы клуба
function getMinTime (select_date, dayOfWeek) {
    if (dayOfWeek == 0 || dayOfWeek == 6) {
        return weekendsTimeStart;
    } else {
        return weekdaysTimeStart;
    }
}
//Максимальное время работы клуба
function getMaxTime (select_date, dayOfWeek) {
    if (dayOfWeek == 0 || dayOfWeek == 6) {
        return weekendsTimeEnd;
    } else {
        return weekdaysTimeEnd;
    }
}
//получение занятых часов с БД
function addDurationLockedIntervals (select_date, dayOfWeek, disableTimeRanges, duration) {
    if (!disableTimeRanges) {
        return disableTimeRanges;
    }
    duration = parseInt(duration);
    let added = [];
    let sortedTimes = [...disableTimeRanges];
    sortedTimes.sort();
    let minTime = getMinTime(select_date, dayOfWeek);
    let maxTime = getMaxTime(select_date, dayOfWeek);
    let maxTimeInt = parseInt(maxTime.substring(0, 2));
    for (let i = parseInt(minTime.substring(0, 2)); i <= maxTimeInt; i++) {
        let lastBusy = i + duration;
        if (sortedTimes.length > 0 && i >= parseInt(sortedTimes[0][1].substring(0, 2))) {
            sortedTimes.shift();
        }
        if (
            (
                (sortedTimes.length > 0)
                &&
                (
                    (lastBusy > parseInt(sortedTimes[0][0].substring(0, 2)))
                    &&
                    (i < parseInt(sortedTimes[0][0].substring(0, 2)))
                )
            ) 
            || 
            (
                (lastBusy > maxTimeInt && maxTime != '23:59') || (maxTime == '23:59' && lastBusy > 24)
            )
        ){
            let nextI = i + 1;
            let time = `${i}:00`;
            let nextTime = `${nextI}:00`;
            if (i < 10) {
                time = `0${i}:00`;
            }
            if (nextI < 10) {
                nextTime = `0${nextI}:00`;
            }
            if (nextI > 23) {
                nextTime = `23:59`;
            }
            added.push([time.toString(), nextTime.toString()]);
        }
    }
    return disableTimeRanges.concat(added);
};
//Вывод свободных часов относительно длительности
function getDisabledTimes (busyTimes, select_date) {
    let res = [];
    let date = new Date().toLocaleString('en-us', {year: 'numeric', month: '2-digit', day: '2-digit'}).
        replace(/(\d+)\/(\d+)\/(\d+)/, '$2.$1.$3');
    let time = new Date();
    if (select_date == date) {
        let startTime = '08:00';
        if (("0" + time.getHours()).slice(-2) > 8) {
            let endTime = ("0" + time.getHours()).slice(-2) + ":" + ("0" + time.getMinutes()).slice(-2);
            res.push([startTime, endTime]);
        }
    }
    if (!busyTimes[select_date]) {
        return res
    } else {
        let times = busyTimes[select_date];
        for (let i = 0; i <= times.length - 2; i+=2) {
            res.push(
                [
                    times[i], times[i + 1]
                ]
            );
        }
        return res;
    }
};

//вывод Timepicker
function generateTimePicker(busyTimes) {
    select_date = $('#datepicker').val();
    let dayOfWeek = new Date(select_date.replace(/(\d+).(\d+).(\d+)/,"$3/$2/$1")).getDay();
    let disableTimeRanges = getDisabledTimes(busyTimes, select_date);
    disableTimeRanges = addDurationLockedIntervals(select_date, dayOfWeek, disableTimeRanges, $('#duration').val());
    //-- Timepicker --
    $('#timepicker').timepicker({
        step: 60,
        disableTimeRanges,
        timeFormat: 'H:i',
        minTime: (select_date) => getMinTime(select_date, dayOfWeek),
        maxTime: (select_date) => getMaxTime(select_date, dayOfWeek),
    });
}

$(document).ready(function () {
//МОДАЛЬНОЕ ОКНО
	GetGameDetails = (url) => {
        try {
            $.ajax({
                type: 'GET',
                url: url,
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#dialogContent').html(res);
                    $('#Modal').modal('show');
                },
                error: function (err) {
                    console.log(err)
                }
            })
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }
});


