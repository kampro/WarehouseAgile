/// <reference path="jquery-1.7.1.min.js" />
/// <reference path="jquery-ui-1.10.2.custom.min.js" />

$(document).ready(function () {
    $("#make-select").change(function () {
        $("#make-form").submit();
    });
    $("#model-select").change(function () {
        $("#model-form").submit();
    });
    $("#makes-section, #models-section, #equipments-section").hide();
    $("#makes-header").click(function () {
        showSection("#makes-section");
    });
    $("#models-header").click(function () {
        showSection("#models-section");
    });
    $("#equipments-header").click(function () {
        showSection("#equipments-section");
    });
    $(".ui-state-default").hover(
		function () { $(this).addClass("ui-state-hover"); },
		function () { $(this).removeClass("ui-state-hover"); }
	);
});

function saveModelSuccess() {
    showResponse("#savemodel-rsp", "<div class=\"success\">Dane zostały zapisane</div>");
}
function saveModelError() {
    showResponse("#savemodel-rsp", "<div class=\"error\">Wystąpił błąd</div>");
}
function showResponse(element, message) {
    $(element).hide();
    $(element).html(message);
    $(element).fadeIn({ "duration": 400, "queue": false });
    $(element).delay(3000).fadeOut({ "duration": 400, "queue": true });
}
function showSection(section) {
    if ($(section).is(":hidden"))
        $(section).fadeIn(400);
    else
        $(section).fadeOut(400);
}