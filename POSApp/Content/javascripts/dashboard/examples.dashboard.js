/*
Name: 			Dashboard - Examples
Written by: 	Okler Themes - (http://www.okler.net)
Theme Version: 	1.5.2
*/
var flotDashSales1;
var flotDashSales2;
var flotDashSales3;
var options = {
    series: {
        lines: {
            show: true,
            lineWidth: 2
        },
        points: {
            show: true
        },
        shadowSize: 0
    },
    grid: {
        hoverable: true,
        clickable: true,
        borderColor: 'rgba(0,0,0,0.1)',
        borderWidth: 1,
        labelMargin: 15,
        backgroundColor: 'transparent'
    },
    yaxis: {
        min: 0,
        color: 'rgba(0,0,0,0.1)'
    },
    xaxis: {
        mode: 'categories',
        color: 'rgba(0,0,0,0)'
    },
    legend: {
        show: false
    },
    tooltip: true,
    tooltipOpts: {
        content: '%x: %y',
        shifts: {
            x: -30,
            y: 25
        },
        defaultTheme: false
    }
};
(function($) {

	'use strict';

	/*
	Sales Selector
	*/
	$('#salesSelector').themePluginMultiSelect().on('change', function() {
		var rel = $(this).val();
		$('#salesSelectorItems .chart').removeClass('chart-active').addClass('chart-hidden');
		$('#salesSelectorItems .chart[data-sales-rel="' + rel + '"]').addClass('chart-active').removeClass('chart-hidden');
	});

	$('#salesSelector').trigger('change');

	$('#salesSelectorWrapper').addClass('ready');







	/*
	Flot: Sales 1
	*/
    flotDashSales1 = $.plot('#flotDashSales1', flotDashSales1Data, options);

	/*
	Flot: Sales 2
	*/
     flotDashSales2 = $.plot('#flotDashSales2', flotDashSales2Data, options);

	/*
	Flot: Sales 3
	*/
     flotDashSales3 = $.plot('#flotDashSales3', flotDashSales3Data, options);





}).apply(this, [jQuery]);
function changeLineGraphData(dataGet) {
    var data2 = [];
    $.each(dataGet[0].data,
        function(index, value) {
            data2.push([value.Month, value.Value]);
        });
    var data3 = [];
    $.each(dataGet[1].data,
        function (index, value) {
            data3.push([value.Month, value.Value]);
        });
    var data4 = [];
    $.each(dataGet[2].data,
        function (index, value) {
            data4.push([value.Month, value.Value]);
        });
    var s = [
         {
            data: data3,
            color: "#0aaa5a"
        },
        {
            data: data4,
            color: "#bbb"
        },
        {
            data: data2,
            color: "#d2322d"
        }
    ];
    $.plot('#flotDashSales1', s, options);


}