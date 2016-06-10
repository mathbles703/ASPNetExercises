$(function () {
    // display message if modal still loaded i
    if ($("#detailsId").val() > 0) {
        var Id = $("#detailsId").val();
        CopyToModal(Id);
        $('#details_popup').modal('show');
    }
    //register
    if ($("#register_popup") != undefined) {
        $('#register_popup').modal('show');
    }
    //login
    if ($("#login_popup") != undefined) {
        $('#login_popup').modal('show');
    }
    // details anchor click - to load popup on catalogue
    $("a.btn-default").on("click", function (e) {
        var Id = $(this).attr("data-id");
        $("#results").text("");
        CopyToModal(Id);
    });
    $("#jsondatartns").on("click", function (e) {
        busySignal("/Data/Json");
    });
    $("#csvdatartns").on("click", function (e) {
        busySignal("/Data/Csv");
    });
});
function CopyToModal(id) {
    $("#qty").val("0");
    $("#cal").text($("#mcal" + id).val());
    $("#carb").text($("#mcarb" + id).val());
    $("#chol").text($("#mchol" + id).val());
    $("#fat").text($("#mfat" + id).val());
    $("#fibre").text($("#mfbr" + id).val());
    $("#pro").text($("#mpro" + id).val());
    $("#salt").text($("#msalt" + id).val());
    $("#description").text($("#descr" + id).data("description"));
    $("#detailsGraphic").attr("src", "/img/burger.jpg");
    $("#detailsId").val(id);
}

function busySignal(url)
{
    var busyImg = $('<img/>', {
        src: "/img/wait.gif"
    });
    $("#busy").empty();
    $("#busy").append(busyImg);
    window.location.href = url;
}