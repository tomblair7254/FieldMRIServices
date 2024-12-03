console.log('Custom styles script loaded.');

function applyRowStyle(rowIndex, className) {
    console.log(`Applying row style: rowIndex=${rowIndex}, className=${className}`);
    var row = document.querySelector(`tr[data-rowindex='${rowIndex}']`);
    if (row) {
        console.log(`Row found: ${row}`);
        row.classList.add(className);
    } else {
        console.log(`Row not found for rowIndex=${rowIndex}`);
    }
}

function applyCellStyle(rowIndex, colIndex, className) {
    console.log(`Applying cell style: rowIndex=${rowIndex}, colIndex=${colIndex}, className=${className}`);
    var cell = document.querySelector(`tr[data-rowindex='${rowIndex}'] td[data-colindex='${colIndex}']`);
    if (cell) {
        console.log(`Cell found: ${cell}`);
        cell.classList.add(className);
    } else {
        console.log(`Cell not found for rowIndex=${rowIndex} and colIndex=${colIndex}`);
    }
}