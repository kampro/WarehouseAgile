
// Documentation for charts library - http://g.raphaeljs.com/ 

function DrawChart(values, labels) {
    var legend = [];
    $.each(labels, function (index, value) {
        legend[index] = "%%.%% " + value;
    });

    var r = Raphael(20, 30, 800, 600);
    var pie = r.piechart(320, 240, 150, values, { legend: legend, legendpos: "east" });

    pie.hover(function () {
        this.sector.stop();
        this.sector.scale(1.1, 1.1, this.cx, this.cy);

        if (this.label) {
            this.label[0].stop();
            this.label[0].attr({ r: 7.5 });
            this.label[1].attr({ "font-weight": 800 });
        }
    }, function () {
        this.sector.animate({ transform: 's1 1 ' + this.cx + ' ' + this.cy }, 500, "bounce");

        if (this.label) {
            this.label[0].animate({ r: 5 }, 500, "bounce");
            this.label[1].attr({ "font-weight": 400 });
        }
    });
}