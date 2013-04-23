/// <reference path="jquery-1.7.1.min.js" />
/// <reference path="jquery-ui-1.10.2.custom.min.js" />

$(document).ready(function () {
    $("#sellers-select").change(function () {
        $("#sellers-form").submit();
    });

    $(".ui-state-default").hover(
		function () { $(this).addClass("ui-state-hover"); },
		function () { $(this).removeClass("ui-state-hover"); }
	);

    //Event bubbling
    $("body").on("click", "a", function (e) {
        var obj = $(e.currentTarget); // === $(this)
        var attrib = obj.attr("rel");

        if (attrib === "delete-seller") {
            return deleteSeller(obj);
        }

        return true;
    });
});

// Shared functions
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
    addSellerSuccess();
}
function addSellerError() {
    showResponse("#saveseller-rsp", "<div class=\"error\">Wystąpił błąd</div>");
}
function saveSellerError() {
    showResponse("#saveseller-rsp", "<div class=\"error\">Wystąpił błąd</div>");
}
function deleteSeller(e) {
    if (confirm("Potwierdź usunięcie sprzedawcy oraz skojarzonych z nim obiektów")) {
        $.ajax({
            url: $(e).attr("href"),
            dataType: "html",
            success: function () {
                addSellerSuccess();
            },
            error: function () {
                saveSellerError();
            }
        });
    }

    return false;
}
