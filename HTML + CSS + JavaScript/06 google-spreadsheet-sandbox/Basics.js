const { GoogleSpreadsheet } = require('google-spreadsheet');

start();
async function start() {
const doc = new GoogleSpreadsheet('11QLw-rY3z186hybTlI9QkXDvgLGz39o3_15HOXryqkw');
doc.useApiKey('AIzaSyANRAmPJFTjvI2lxfJpq82rd4SHtpBdKY0');
await doc.loadInfo();
console.log("Hello world");
console.log('https://www.npmjs.com/package/google-spreadsheet')
console.log(doc.title)
}