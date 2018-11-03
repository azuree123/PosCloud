/*
Name: 			UI Elements / Charts - Examples
Written by: 	Okler Themes - (http://www.okler.net)
Theme Version: 	1.5.2
*/

(function($) {

	'use strict';

	

	/*
	Morris: Line
	*/
	//Morris.Line({
	//	resize: true,
	//	element: 'morrisLine',
	//	data: morrisLineData,
	//	xkey: 'y',
	//	ykeys: ['a', 'b'],
	//	labels: ['Series A', 'Series B'],
	//	hideHover: true,
	//	lineColors: ['#0088cc', '#734ba9'],
	//});

	/*
	Morris: Donut
	*/
	//Morris.Donut({
	//	resize: true,
	//	element: 'morrisDonut',
	//	data: morrisDonutData,
	//	colors: ['#0088cc', '#734ba9', '#E36159']
	//});

	/*
	Morris: Bar
	*/
	Morris.Bar({
		resize: true,
		element: 'morrisBar',
		data: morrisBarData,
		xkey: 'y',
		ykeys: ['a', 'b'],
		labels: ['Sales', 'Expenses'],
		hideHover: true,
        barColors: ['#e0534e', '#08bb62']
    });


	/*
	Chartist: Bar Chart - Horizontal Chart
	*/
    (function () {
        new Chartist.Bar('#ChartistHorizontalChart', {
            labels: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'],
            series: [
                [5, 4, 3, 7, 5, 10, 3],
                [3, 2, 9, 5, 4, 6, 4]
            ]
        }, {
                seriesBarDistance: 10,
                reverseData: true,
                horizontalBars: true,
                axisY: {
                    offset: 70
                }
            });
    })();

	/*
	Morris: Area
	*/
	//Morris.Area({
	//	resize: true,
	//	element: 'morrisArea',
	//	data: morrisAreaData,
	//	xkey: 'y',
	//	ykeys: ['a', 'b'],
	//	labels: ['Series A', 'Series B'],
	//	lineColors: ['#0088cc', '#2baab1'],
	//	fillOpacity: 0.7,
	//	hideHover: true
	//});

	/*
	Morris: Stacked
	*/
	//Morris.Bar({
	//	resize: true,
	//	element: 'morrisStacked',
	//	data: morrisStackedData,
	//	xkey: 'y',
	//	ykeys: ['a', 'b'],
	//	labels: ['Series A', 'Series B'],
	//	barColors: ['#0088cc', '#2baab1'],
	//	fillOpacity: 0.7,
	//	smooth: false,
	//	stacked: true,
	//	hideHover: true
	//});

	
	

}).apply(this, [jQuery]);