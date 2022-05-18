//-- Datepicker --
(function($) {
    ////-- Datepicker --
        $("#datepicker").datepicker({
            //numberOfMonths: 2,
            //showButtonPanel: true,
            minDate: 0,
            maxDate: "+30D"
        });

    let datePicker = $('#datepicker').val();
    let continuance = $('#continuance').val();
    //Провкряем вибрана дата
    if (datePicker == "") {
        $('#timepicker').prop('disabled', true);    //блокируем выбор времени если нет
    }
    $('#datepicker').on('change', () => $('#timepicker').prop('disabled', false));

    //TODO:сцука не працює
    if (continuance == "") {
        $('#timepicker').prop('disabled', true);
    }
    $('#continuance').on('change', () => $('#timepicker').prop('disabled', false));
})(jQuery);

