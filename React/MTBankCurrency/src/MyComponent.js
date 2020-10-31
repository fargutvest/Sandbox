import React from 'react';
import axios from 'axios';
import parser from 'xml2json-light';
import {CanvasJSChart} from 'canvasjs-react-charts'

var dataPoints =[];

function addDay(value) {
    return new Date(value.setDate(value.getDate() + 1))
}

function ddMMYYYY(date){
    return `${date.getDate()}.${date.getMonth() + 1 }.${date.getFullYear() }`;
}

function getCurrency(xmlRaw){
    var jObj = parser.xml2json(xmlRaw);
    var currency = jObj.rates.currency; 
    var usd_byn = 0;
    try{
        currency.forEach(element => {
            if (element.code == "USD" && element.codeTo == "BYN"){
                usd_byn = parseFloat(element.purchase);
            }
        });
    }
    catch(ex){
        console.log(ex);
    }
    
    return usd_byn;
}

function dataPointSort(dataPointA, dataPointB){
    return dataPointA.x > dataPointB.x
}

class MyComponent extends React.Component {


    constructor(props) {
        super(props);
        this.state = { };
    }

    componentDidMount() {
        var values = [];
        var from = new Date('2020-05-01');
        var item = from;
        
        for (let index = 0; index < 190; index++) {
            values.push({
               date: item,
               dateStr: ddMMYYYY(item)
            });
            item = addDay(item);
        }
        console.log(values);

        var chart = this.chart;
        var self = this;

        var valuesXml = [];

        values.forEach(dateItem => {
           axios.get(`https://www.mtbank.by/currxml.php?d=${dateItem.dateStr}`, {
                "Content-Type": "application/xml; charset=utf-8"
            })
            .then(response => {
                var xmlText = response.data;
                var currency = getCurrency(xmlText);
                if (currency> 0){
                    dataPoints.push({
                        x: dateItem.date,
                        y: currency
                    });
                    dataPoints = dataPoints.sort(dataPointSort);
                    chart.render();
                }
            });
        });
      
        console.log(valuesXml);

        
        
		
    }

    render() {
        const options = {
			theme: "light2",
			title: {
				text: "MTBank USD/BYN"
			},
			axisY: {
				title: "BYN",
				prefix: ""
			},
			data: [{
				type: "line",
				xValueFormatString: "MMM YYYY",
				yValueFormatString: "$#,##0.00",
				dataPoints: dataPoints
			}]
		}
		return (
		<div>
			<CanvasJSChart options = {options} 
				 onRef={ref => this.chart = ref}
			/>
        </div>
        )
    }
}

export default MyComponent;