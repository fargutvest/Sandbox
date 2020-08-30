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
const sheet = doc.sheetsByIndex[0];
console.log(sheet.title);
await sheet.loadCells('A1:Z1000');
const a1 = sheet.getCell(0, 0);

var currentdate = new Date(); 
var datetime =  currentdate.getDate() + "/"
                + (currentdate.getMonth()+1)  + "/" 
                + currentdate.getFullYear() + " @ "  
                + currentdate.getHours() + ":"  
                + currentdate.getMinutes() + ":" 
                + currentdate.getSeconds();

console.log(a1.value);
a1.value = datetime;
a1.textFormat = { bold: true };
await sheet.saveUpdatedCells(); // save all updates in one call

const newSheet = await doc.addSheet({ title: datetime });
console.log(datetime);
}