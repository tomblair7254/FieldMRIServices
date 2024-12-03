function Scroll() {
    var scroll = document.getElementById('wrapper1');
    var grid = document.getElementsByClassName('e-grid')[0].ej2_instances[0];
    var content = document.getElementsByClassName('e-gridcontent')[0]; // Get content     

    if (scroll && content) {
        grid.element.insertBefore(scroll, content); // Place the scrollbar between the content and header    

        var wrapper1 = document.getElementById("wrapper1");
        wrapper1.style.display = "block";

        // Adjust width of the scrollbar wrapper
        document.getElementById("div1").style.width = content.firstElementChild.scrollWidth + "px";

        // Bind onscroll event to the wrapper
        wrapper1.onscroll = function () {
            content.firstElementChild.scrollLeft = wrapper1.scrollLeft; // Sync wrapper1 scrollLeft with the Grid scrollLeft
        };

        // Bind onscroll event to the content
        content.firstElementChild.onscroll = function () {
            wrapper1.scrollLeft = content.firstElementChild.scrollLeft; // Sync Grid scrollLeft with the wrapper1 scrollLeft
        };
    }
}
highlightRow1 = function (rowIndex, colorValue) {
    setTimeout(function () {
        var rows = document.querySelectorAll('.e-row');
        var row = rows[rowIndex];

        if (row) {
            var cells = row.cells;

            for (var i = 0; i < cells.length; i++) {
                cells[i].style.backgroundColor = colorValue;
            }
        }
    }, 200);
};


highlightcell = function (colIndex, rowindex, colorValue1) {

    setTimeout(function () {

        var a = document.getElementsByClassName("e-row")[rowindex];

        var b = a.getElementsByClassName('e-rowcell')[colIndex].style.backgroundColor = colorValue1;
    }, 200);
};


