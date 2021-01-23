import React, { useState, useEffect } from 'react'
import axios from 'axios'
import parser from 'xml2json-light'
import { CanvasJSChart } from 'canvasjs-react-charts'

function App() {

    const [dataPoints, setDataPoints] = useState([]);

    const delay = ms => {
        return new Promise(r => setTimeout(() => r(), ms))
    }

    function parseCurrency(xmlRaw) {
        let jObj = parser.xml2json(xmlRaw)
        let currency = jObj.rates.currency
        let usd_byn = 0
        try {
            currency.forEach(element => {
                if (element.code == "USD" && element.codeTo == "BYN") {
                    usd_byn = parseFloat(element.purchase)
                }
            })
        }
        catch (ex) {
            console.log(ex)
        }

        return usd_byn
    }

    async function fetchPoints(dates) {
        let points = []
        for (let i = 0; i < dates.length; i++) {
            try {
                let response = await axios.get(`https://www.mtbank.by/currxml.php?d=${dates[i].dateStr}`, {
                    "Content-Type": "application/xml charset=utf-8"
                })

                let currency = parseCurrency(response.data)
                if (currency > 0) {
                    points.push({ x: dates[i].date, y: currency })
                }
            }
            catch (err) {
                console.log('error', err)
                break;
            }
        }
        return points
    }

    async function fetchMockPoints(dates, ms = 1000) {
        let points = []
        await delay(ms);
        for (let i = 0; i < dates.length; i++) {
            if (points.length === 0) {
                for (let i = 0; i < dates.length; i++) {
                    points.push({ x: dates[i].date, y: Math.random() })
                }
            }
        }
        return points
    }

    useEffect(async ()=> {
        let dates = []
        let item = new Date('2020-12-01')

        for (let index = 0; index < 54; index++) {
            dates.push({ date: item, dateStr: `${item.getDate()}.${item.getMonth() + 1}.${item.getFullYear()}` })
            item = new Date(item.setDate(item.getDate() + 1))
        }
        let points = await fetchPoints(dates).then(points => { return points.sort((a, b) => a.x > b.x) });
        console.log('points', points)
        setDataPoints(points);
    },[]);

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
            <CanvasJSChart options={options} />
        </div>
    )
}

export default App