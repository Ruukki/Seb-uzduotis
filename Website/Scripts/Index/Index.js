$(function () {
    $('#getInfo').on('click', function(e) {
        e.preventDefault();
        var el = $(this);
        var target = $('#loadContent');
        $.ajax({
            method: "GET",
            url: el.attr("href"),
            data: { agreementId: $('#Agreements').val(), newBaseRateCode: $('#BaseRateCodes').val() },
            beforeSend: function () {
                target.empty();
                target.append("Loading...");
            }
        }).done(function(data) {
            target.empty();
            target.append(data);
        });
    });
});