﻿function hide() {
    $("#show_button").show();
    $("#search_panel").hide("drop", { direction: "left" }, "slow");

}


function show() {
    $("#search_panel").show("drop", { direction: "left" }, "slow");
    $("#show_button").hide();
}


function upcomingChanged(cb) {

    var isChecked = $(cb).prop("checked");
    var isCheckedPast = $("#isPast").prop("checked");

    if (isChecked) {
        if (!isCheckedPast)
            $("#fromDate").val(getCurrentDate());
        $("#toDate").val("2100-01-01");

    } else {
        if (isCheckedPast) $("#fromDate").val("1900-01-01");
        $("#toDate").val(getCurrentDate());

    }

}

function pastChanged(cb) {

    var isChecked = $(cb).prop("checked");
    var isCheckedUpcoming = $("#isUpcoming").prop("checked");

    if (isChecked) {
        if (!isCheckedUpcoming)
            $("#toDate").val(getCurrentDate());
        $("#fromDate").val("1900-01-01");

    } else {
        if (isCheckedUpcoming) $("#toDate").val("2100-01-01");
        $("#fromDate").val(getCurrentDate());

    }
}

function datesChanged(cb) {
    var isChecked = $(cb).prop("checked");

    if (isChecked) {
        $("#isUpcoming").attr("disabled", "disabled");
        $("#isPast").attr("disabled", "disabled");
        $("#toDate").attr('readonly', false);
        $("#fromDate").attr('readonly', false);
    } else {
        $("#isUpcoming").removeAttr("disabled");
        $("#isPast").removeAttr("disabled");
        $("#fromDate").attr('readonly', true);
        $("#toDate").attr('readonly', true);
    }
}

function showStat() {
    $("#stat_panel").show("drop", { direction: "right", complete: statPanelShowed }, "slow");
    $("#stat_button").hide();
}

function hideStat() {
    $("#stat_button").show();
    $("#stat_panel").hide("drop", { direction: "right", complete: statPanelHided }, "slow");
}

function statPanelShowed() {

    //$("regionChart").css("display","block");

    var regionChart = createChart("regionChart", "Region stat", JSON.parse(localStorage.getItem("regionStat")), getHeight(localStorage.getItem("regionStatCount")));

    var countryChart = createChart("countryChart", "Country Stat", JSON.parse(localStorage.getItem("countryStat")), getHeight(localStorage.getItem("countryStatCount")));

    var cityChart = createChart("cityChart", "City Stat", JSON.parse(localStorage.getItem("cityStat")), getHeight(localStorage.getItem("cityStatCount")));

    regionChart.render();
    countryChart.render();
    cityChart.render();

}

function getHeight(count) {
    return count * 20;
}

function createChart(containerName, titleText, dataPoints, height) {

    var chart = new CanvasJS.Chart(containerName, {
        title: {
            text: titleText,
            fontFamily: "Verdana",
            fontColor: "Peru",
            fontSize: 28

        },
        width: 300,
        height: height,
        animationEnabled: true,
        backgroundColor: '#edf7ee',
        axisY: {
            tickThickness: 0,
            lineThickness: 0,
            valueFormatString: " ",
            gridThickness: 0
        },
        axisX: {
            tickThickness: 0,
            lineThickness: 0,
            labelFontSize: 10,
            labelFontColor: "Peru",
            interval: 1
        },
        data: [
        {
            indexLabelFontSize: 12,
            toolTipContent: "<span style='\"'color: {color};'\"'><strong>{label}</strong></span>",

            indexLabelPlacement: "inside",
            indexLabelWrap: true,
            indexLabelFontColor: "white",
            indexLabelFontWeight: 100,
            indexLabelFontFamily: "Verdana",
            color: "#62C9C3",
            type: "bar",
            dataPoints: dataPoints
        }
        ]
    });

    return chart;
}

function statPanelHided() {

    //$("regionChart").css("display","none");

}

function getCurrentDate() {

    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1;

    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }
    var today = yyyy + "-" + mm + "-" + dd;

    return today;
}