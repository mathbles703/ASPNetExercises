$(function () {
    // display message if modal still loaded i
    if ($("#detailsId").val() > 0) {
        var Id = $("#detailsId").val();
        CopyToModal(Id);
        $('#details_popup').modal('show');
    } //details
    // details anchor click - to load popup on catalogue
    $("a.btn-default").on("click", function (e) {
        var Id = $(this).attr("data-id");
        $("#results").text("");
        CopyToModal(Id);
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