/// <reference path="jquery-1.7.1.min.js" />
/// <reference path="jquery-ui-1.10.2.custom.min.js" />

$(document).ready(function () {
    $("#makes-select").change(function () {
        $("#makes-form").submit();
    });

    $(".ui-state-default").hover(
		function () { $(this).addClass("ui-state-hover"); },
		function () { $(this).removeClass("ui-state-hover"); }
	);

    //Event bubbling
    $("body").click(function (e) {
        var target = e.target;

        if (isClickContained("a[rel=\"delete-equipment-model\"]", target)) {
            return deleteEquipmentModel(chooseTarget(target));
        }

        return true;
    });
});

// Shared functions
function isClickContained(element, clicked) {
    if ($(clicked).parent().is(element) || $(clicked).is(element))
        return true;
    else
        return false;
}
function chooseTarget(element) {
    if ($(element).is("a"))
        return $(element);
    else
        return $(element).parent();
}
function showResponse(element, message) {
    $(element).hide();
    $(element).html(message);
    $(element).fadeIn({ duration: 400, queue: false });
    $(element).delay(3000).fadeOut({ duration: 400, queue: true });
}

// Sellers functions
function addSellerSuccess() {
    $.ajax({
        url: "/Sellers/GetSellers",
        dataType: "html",
        success: function (data) {
            $("#sellers-select").html(data);
            showResponse("#saveseller-rsp", "<div class=\"success\">Dane zostały zapisane</div>");
        },
        error: function () {
            showResponse("#saveseller-rsp", "<div class=\"error\">Wystąpił błąd</div>");
        }
    });
}
function saveSellerSuccess() {
    addColorSuccess();
}
function addSellerError() {
    showResponse("#saveseller-rsp", "<div class=\"error\">Wystąpił błąd</div>");
}
function saveSellerError() {
    showResponse("#saveseller-rsp", "<div class=\"error\">Wystąpił błąd</div>");
}
function deleteColor(e) {
    if (confirm("Potwierdź usunięcie koloru oraz skojarzonych z nim obiektów - usuwanie koloru jest NIEZALECANE (zostaną usunięte skojarzone z nim zamówienia)")) {
        $.ajax({
            url: $(e).attr("href"),
            dataType: "html",
            success: function () {
                addColorSuccess();
            },
            error: function () {
                saveColorError();
            }
        });
    }

    return false;
}
