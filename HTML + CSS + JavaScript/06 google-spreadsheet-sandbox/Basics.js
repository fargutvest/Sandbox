const { GoogleSpreadsheet } = require('google-spreadsheet');
const credentials = require('./config/quickstart-1554057009071-aa2574f181ba.json');
const spreadsheetID = '1AzLrSQ2-L-AKOphu3ePNqbAzujkLTyeCKXibMjRYqV8';

start();
async function start() {
console.log("Hello google-spreadsheet");
console.log('https://www.npmjs.com/package/google-spreadsheet')

const doc = new GoogleSpreadsheet(spreadsheetID);
//doc.useApiKey('AIzaSyANRAmPJFTjvI2lxfJpq82rd4SHtpBdKY0');
console.log(credentials);
doc.useServiceAccountAuth(credentials);
await doc.loadInfo();
console.log(doc.title)

var currentdate = new Date(); 
var datetime =  currentdate.getDate() + "/"
                + (currentdate.getMonth()+1)  + "/" 
                + currentdate.getFullYear() + " @ "  
                + currentdate.getHours() + ":"  
                + currentdate.getMinutes() + ":" 
                + currentdate.getSeconds();

const newSheet = await doc.addSheet({ title: datetime });
console.log(datetime);
}