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
    var data = JSON.parse($("#menuitem" + id).val());
    $("#qty").val("0");
    $("#cal").text(data.CAL);
    $("#carb").text(data.CARB);
    $("#chol").text(data.CHOL) ;
    $("#fat").text(data.FAT);
    $("#fibre").text(data.FBR);
    $("#pro").text(data.PRO);
    $("#salt").text(data.SALT);
    $("#description").text(data.Description);
    $("#detailsGraphic") .attr("src", "/img/burger.jpg");
    $("#detailsId").val(id);
}