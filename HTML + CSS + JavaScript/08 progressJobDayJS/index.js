function start() {
    var begin = new Date();
    begin.setHours(10);
    begin.setMinutes(0);

    var end = new Date();
    end.setHours(19);
    end.setMinutes(0);

    var fullDayMs = end.getTime() - begin.getTime();

    var elem = document.getElementById("myBar");
    frame();
    setInterval(frame, 1000);
    function frame() {
        var current = new Date();
        var elapsedMs = current.getTime() - begin.getTime();
        var elapsedPercent = elapsedMs / fullDayMs * 100 + "%";
        elem.style.width = elapsedPercent;
        document.getElementById("myOutput").value = elapsedPercent;
      }
    }
  