/* /*
	Система загружает выпадающий список с доступным временем начала сеанса исходя из ранее указанных Пользователем данных:
	Дата заказа и длительность, а также график работы клуба.
*/
/*
const weekdaysTimeStart = '08';
const weekdaysTimeEnd = '22';
const weekendsTimeStart = '09';
const weekendsTimeEnd = '23:59';

function getMinTime (select_date, dayOfWeek) {
    if (dayOfWeek == 0 || dayOfWeek == 6) {
        return weekendsTimeStart;
    } else {
        return weekdaysTimeStart;
    }
}

function getMaxTime (select_date, dayOfWeek) {
    if (dayOfWeek == 0 || dayOfWeek == 6) {
        return weekendsTimeEnd;
    } else {
        return weekdaysTimeEnd;
    }
}

function getDisabledTimes (busyTimes, select_date) {
    let res = [];
    if (select_date == '@DateTime.Now.Date.ToString("d")') {
        let startTime = '08:00';
        if (@DateTime.Now.TimeOfDay.Hours > 8) {
            let endTime = '@DateTime.Now.TimeOfDay.ToString(@"hh\:mm")';
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
    console.log(disableTimeRanges);
    console.log(added);
    return disableTimeRanges.concat(added);
};

function generateTimePicker(busyTimes) {
    select_date = $('#datepicker').val();
    let dayOfWeek = new Date(select_date.replace(/(\d+).(\d+).(\d+)/,"$3/$2/$1")).getDay();
    console.log("select_date - " + select_date);
    let disableTimeRanges = getDisabledTimes(busyTimes, select_date);
    disableTimeRanges = addDurationLockedIntervals(select_date, dayOfWeek, disableTimeRanges, $('#duration').val());
    console.log(disableTimeRanges);
    //-- Timepicker --
    $('#timepicker').timepicker({
        step: 60,
        disableTimeRanges,
        timeFormat: 'H:i',
        minTime: (select_date) => getMinTime(select_date, dayOfWeek),
        maxTime: (select_date) => getMaxTime(select_date, dayOfWeek),
    });
}

$(document).ready(function() {
    let date_selected = false;
    let duration_selected = false;
    let select_date;
  
    let textDisableTimeRanges = "";
    const busyTimes = {};
    @foreach (var data in Model.DictionarySessions) {
        @:busyTimes['@data.Key'] = [];
        foreach (var item in data.Value)
        {
            @:busyTimes['@data.Key'].push('@item');
        }
    }
    
    //-- Datepicker --
    $('#datepicker').datepicker({
        minDate: 0,
        maxDate: "+30D"
    });

    //lock timepicker
    $('#timepicker').prop('disabled', true);    
    //unlock timepicker
    $('#datepicker').on('change', () => {
        if ($('#datepicker').val()) date_selected = true;
        //console.log(dayOfWeek);
        if (date_selected && duration_selected) {
            generateTimePicker(busyTimes);
            $('#timepicker').prop('disabled', false)
        }
    });
    $('#duration').on('change', () => {
        if ($('#duration').val()) duration_selected = true;
        
        if (date_selected && duration_selected) {
            generateTimePicker(busyTimes);
            $('#timepicker').prop('disabled', false)
        }
    });
}); 
*/