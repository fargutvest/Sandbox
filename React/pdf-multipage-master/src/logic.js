import jsPDF from 'jspdf';

const a4WidthMm = 210;
const a4HeightMm = 297;

const a4WidthInches = 8.3;
const a4HeightInches = 11.7;

const a4WidthPixels72PPI = 595;
const a4HeightPixels72PPI = 842;

const margins = {
    top: 10,
    bottom: 10,
    left: 0,
    width: 297
};

const pxToMm = (px) => {
    return Math.floor(px / document.getElementById('myMm').offsetHeight);
};

const mmToPx = (mm) => {
    return document.getElementById('myMm').offsetHeight * mm;
};

function setFont(doc) {
    doc.setFontSize(12);
    doc.setTextColor(40);
    doc.setFontStyle('normal');
}

function horizontalLine(doc, y){
    doc.setLineCap(2);
    doc.line(3, y, margins.width - 6, y); // horizontal line
}

function footer(doc, pageNumber, totalPages) {
    let y = doc.internal.pageSize.height - margins.bottom;
    horizontalLine(doc, y)
    setFont(doc);
    var str = "Page " + pageNumber + " of " + totalPages
    doc.text(str, margins.left, doc.internal.pageSize.height - margins.bottom / 2);
};

function header(doc) {
    setFont(doc);
    doc.text("Report Header Template", margins.left, margins.top / 2);
    horizontalLine(doc, margins.top)
};

function headerFooterFormatting(doc, totalPages) {
    for (var i = totalPages; i >= 1; i--) {
        doc.setPage(i);

        header(doc);

        footer(doc, i, totalPages);
        doc.page++;
    }
};

export let onClick = () => {
    var pdf = new jsPDF('l', 'mm', 'a4');
    pdf.setFontSize(18);
    pdf.fromHTML(document.getElementById('html-2-pdfwrapper'),
        margins.left,
        margins.top,
        {
            width: margins.width// max width of content on PDF
        }, function (dispose) {
            headerFooterFormatting(pdf, pdf.internal.getNumberOfPages());
        },
        margins);

    toIframe(pdf);
}

function toIframe(pdf) {
    var iframe = document.createElement('iframe');
    iframe.setAttribute('style', 'position:absolute;right:0; top:0; bottom:0; height:100%; width:650px; padding:20px;');
    document.body.appendChild(iframe);

    iframe.src = pdf.output('datauristring');
}

function toFile(pdf, fileName) {
    pdf.save(fileName);
}