$(document).ready(function () {
    SetPage();
    ChangeDate("btnToday");
});
//#region SetFuncs
function SetPage() {
    $(".leftArrow").hide();
    $("#startPoint").val("35");
    $("#endPoint").val("6");
    $("#startPointText").val($("#startPoint option:selected").text());
    $("#endPointText").val($("#endPoint option:selected").text());
    var input = document.getElementById("date");
    input.setAttribute("min", today);
}
//#endregion
//#region ChangeFuncs
function ChangeDate(id, id2) {
    if (id == "btnToday") { $("#date").val(today); }
    else { $("#date").val(tomorrow); }

    $("#" + id).css("background", "#5D686E");
    $("#" + id).css("color", "white");

    $("#" + id2).css("background", "white");
    $("#" + id2).css("color", "#5D686E");
}
function ChangePoints(event) {
    var startPoint = $("#startPoint").val();
    var endPoint = $("#endPoint").val();

    $("#startPointText").val($("#startPoint option:selected").text());
    $("#endPointText").val($("#endPoint option:selected").text());

    if (startPoint == endPoint) {
        alert("Başlangıç ve bitiş noktaları aynı olamaz. Lütfen seçiminizi kontrol ediniz.");
        if (event == "start") { $("#startPoint").val("0"); } else { $("#endPoint").val("0"); }
    }
}
function ChangeCities() {
    var startPoint = $("#startPoint").val();
    var endPoint = $("#endPoint").val();
    $("#endPoint").val(startPoint);
    $("#startPoint").val(endPoint);
}
function ChangeDatePicker() {
    var date = $("#date").val();
    if (date == today) { ChangeDate("btnToday", "btnTomorrow"); }
    else if (date == tomorrow) { ChangeDate("btnTomorrow", "btnToday"); }
    else {
        $("#btnToday").css("background", "white");
        $("#btnToday").css("color", "#5D686E");
        $("#btnTomorrow").css("background", "white");
        $("#btnTomorrow").css("color", "#5D686E");
    }
}
//#endregion