//async function getNameAd() {
//    const response = await fetch(`${window.location.href}/name`, {
//        method: "GET",
//        headers: { "Accept": "application/json" }
//    });

//    if (response.ok === true) {
//        const adName = await response.json();
//        const h2 = document.getElementsByTagName("h2")[0];
//        h2.append(adName);
//    }
//}

async function getNameAndPositions() {
    const response = await fetch(`${window.location.href}/positions`, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    if (response.ok === true) {
        const adNameAndPositions = await response.json();
        const h2 = document.getElementsByTagName("h2")[0];
        h2.append(adNameAndPositions.name);
        const rows = document.querySelector("tbody");
        adNameAndPositions.positionStatistics.forEach(statistics => rows.append(row(statistics)));
    }
}

function row(statistics) {
    const tr = document.createElement("tr");

    const dateTd = document.createElement("td");
    var date = new Date(statistics.date);
    dateTd.append(date.getDate() + "." + (date.getMonth() + 1));
    tr.append(dateTd);

    var index = 0;
    for (var i = 0; i < 24; i++) {
        const position = document.createElement("td");
        if (statistics.updateHour[index] == i && statistics.positions[index] != 0) {
            if (statistics.positions[index] <= 10)
                position.className = "high-position";
            else if (statistics.positions[index] <= 30)
                position.className = "medium-position";
            else
                position.className = "low-position";
            position.append(statistics.positions[index]);
            index++;
        }
        else {
            position.className = "undefind-position";
            position.append("?");
        }

        tr.append(position);
    }

    return tr;
}

//getNameAd();
getNameAndPositions();