$(function () {
    $("#MakesSelection").change(LoadModels);
    $("#ModelsSelection").change(LoadEquipments);
    $("#ModelsSelection, #EquipmentsSelection").change(LoadPrice);
    $("#EquipmentsSelection").change(Validate);
});

function LoadModels() {
    $.ajax({
        url: "/Offer/GetModels",
        type: "POST",
        data: {
            "make": $("#MakesSelection").val()
        },
        success: function (result) {
            $("#ModelsSelection").html(result);
            LoadEquipments();
        }
    });

    $("#OrderPriceField").text("0 zł");
}

function LoadEquipments() {
    var equipments = "<option value = '0'>Brak</option>";
    $.ajax({
        url: "/Orders/GetEquipmentsByModelId",
        type: "POST",
        data: {
            "modelId": $("#ModelsSelection").val()
        },
        success: function (result) {
            if (result != null) {
                $.each(result, function () {
                    equipments += "<option value = '" + this.Id + "'>" + this.Value + "</option>";
                });
            }
            $("#EquipmentsSelection").html(equipments);
            Validate();
        }
    });
}

function LoadPrice() {
    CountPrice($("#ModelsSelection").val(), $("#EquipmentsSelection").val(), "#OrderPriceField");
}

function CountPrice(modelId, equipmentPriceId, selector) {
    $.ajax({
        url: "/Orders/CountOrderPrice",
        type: "POST",
        data: {
            "modelId": modelId,
            "equipmentPriceId": equipmentPriceId
        },
        success: function (result) {
            $(selector).text(result + " zł");
        }
    });
}

function Validate() {
    if ($("#EquipmentsSelection").val() > 0)
        $("#SaveButton").attr("disabled", null);
    else
        $("#SaveButton").attr("disabled", "disabled");
}