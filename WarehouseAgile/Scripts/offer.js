/// <reference path="jquery-1.9.1.min.js" />
/// <reference path="jquery-ui-1.10.2.custom.min.js" />

$(document).ready(function () {
    $("#make-select").change(function () {
        $("#make-form").submit();
    });
    $("#model-select").change(function () {
        $("#model-form").submit();
    });
});