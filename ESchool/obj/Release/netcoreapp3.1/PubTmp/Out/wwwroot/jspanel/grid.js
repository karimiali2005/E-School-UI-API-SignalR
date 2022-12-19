function chkAllGrid() {
    var chks = document.getElementsByClassName('chkGrid');
    var chkAll = document.getElementById('chkAll');
    for (let chk of chks) {
        chk.checked = chkAll.checked;
    }
}

document.addEventListener("DOMContentLoaded", function () {
     /*makeAllSortable(null);*/

    var getCellValue = function (tr, idx) { return tr.children[idx].innerText || tr.children[idx].textContent; }

    var comparer = function (idx, asc) {
        return function (a, b) {
            return function (v1, v2) {
                return v1 !== '' && v2 !== '' && !isNaN(v1) && !isNaN(v2) ? v1 - v2 : v1.toString().localeCompare(v2);
            }(getCellValue(asc ? a : b, idx), getCellValue(asc ? b : a, idx));
        }
    };

    // do the work...
    Array.prototype.slice.call(document.querySelectorAll('th')).forEach(function (th) {
        th.addEventListener('click', function () {
            var table = th.parentNode
            while (table.tagName.toUpperCase() != 'TABLE') table = table.parentNode;
            Array.prototype.slice.call(table.querySelectorAll('tr:nth-child(n+2)'))
                .sort(comparer(Array.prototype.slice.call(th.parentNode.children).indexOf(th), this.asc = !this.asc))
                .forEach(function (tr) { table.appendChild(tr) });
        })
    });
});

function makeAllSortable(parent) {
    parent = parent || document.body;
    var t = parent.getElementsByTagName('table'), i = t.length;    
    while (--i >= 0) makeSortable(t[i]);
}

function makeSortable(table) {
    
    var th = table.th, i;
    th && (th = th.rows[0]) && (th = th.cells);
    if (th) i = th.length;
    else return; // if no `<thead>` then do nothing
    while (--i >= 0) (function (i) {
        var dir = 1;
        th[i].addEventListener('click', function () { sortTable(table, i, (dir = 1 - dir)) });
    }(i));
}

function sortTable(table, col, reverse) {
    console.log(table);
    var tb = table.tBodies[0], // use `<tbody>` to ignore `<thead>` and `<tfoot>` rows
        tr = Array.prototype.slice.call(tb.rows, 0), // put rows into array
        i;
    reverse = -((+reverse) || -1);
    tr = tr.sort(function (a, b) { // sort rows
        return reverse // `-1 *` if want opposite order
            * (a.cells[col].textContent.trim() // using `.textContent.trim()` for test
                .localeCompare(b.cells[col].textContent.trim())
            );
    });
    for (i = 0; i < tr.length; ++i) tb.appendChild(tr[i]); // append each row in order
}

function filterTable() {
    showLoader();
    var q = document.getElementById('q').value;
    var strs = q.split(' ');
    
    var rows = document.querySelectorAll('table.grid tr.satr');  

    for (let row of rows) {
        row.classList.remove('displayNone');
    }

    for (let row of rows) {

        var has = true;
        var words = replaceForSearch(row.innerHTML);  

        for (let str of strs) {
            str = replaceForSearch(str);            
            if (words.indexOf(str) < 0) {                
                has = false;
                break;
            } 
        }

        if (has) {
            row.classList.remove('displayNone');
        } else {
            row.classList.add('displayNone');
        }

    }
    hideLoader();
}