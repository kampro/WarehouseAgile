/// <reference path="jquery-1.7.1.min.js" />

$(document).ready(function () {
    $("#make-select").change(function () {
        $("#make-form").submit();
    });
    $("#model-select").change(function () {
        $("#model-form").submit();
    });
});