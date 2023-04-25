

async function loadUsers() {
    removeContent();
    createTable();
    const response = await fetch("/admin/users", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    // если запрос прошел нормально
    if (response.ok === true) {
        const users = await response.json();
        const rows = document.querySelector("tbody");
        users.forEach(user => rows.append(row(user)));
    }
}

function removeContent() {
    const content = document.getElementById("content");
    content.replaceChildren();
}

function row(user) {
    const tr = document.createElement("tr");
    tr.setAttribute("data-rowid", user.id);

    const idTd = document.createElement("td");
    const copyBtn = document.createElement("button");
    copyBtn.className = "copyBtn";
    const copyImg = document.createElement("img");
    copyImg.className = "copyImg";
    copyBtn.append(copyImg);
    copyBtn.addEventListener("click", () => navigator.clipboard.writeText(user.id));

    idTd.append(copyBtn);
    tr.append(idTd);


    const nameTd = document.createElement("td");
    nameTd.append(user.name);
    tr.append(nameTd);

    const adsCountTd = document.createElement("td");
    adsCountTd.append(user.adsCount);
    tr.append(adsCountTd);

    return tr;
}

function createTable() {
    const content = document.getElementById("content");
    const table = document.createElement("table");
    const thead = document.createElement("thead");
    const tr = document.createElement("tr");

    const thId = document.createElement("th");
    thId.id = "userId";
    thId.append("ID");
    tr.appendChild(thId);

    const thName = document.createElement("th");
    thName.id = "userName";
    thName.append("Имя");
    tr.appendChild(thName);

    const thAdsCount = document.createElement("th");
    thAdsCount.id = "adsCount";
    thAdsCount.append("Кол-во объявлений");
    tr.appendChild(thAdsCount);

    thead.appendChild(tr);
    table.appendChild(thead);

    const tbody = document.createElement("tbody");
    table.appendChild(tbody);

    content.appendChild(table);
}

async function loadLog() {
    removeContent();
    const response = await fetch("/admin/log", {
        method: "GET",
    });

    if (response.ok === true) {
        const log = await response.text();
        const content = document.getElementById("content");

        const logText = document.createElement("div");
        logText.className = "logTextArea";
        
        logText.innerHTML = log.replaceAll('\r\n', '<br />');
        
        content.appendChild(logText);
        logText.scrollTop = logText.scrollHeight;
    }
}

document.getElementById("users").addEventListener("click", () => loadUsers());

document.getElementById("log").addEventListener("click", () => loadLog());