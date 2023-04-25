// Получение всех объявлений
async function getAds() {
    const response = await fetch("/api/ads", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    // если запрос прошел нормально
    if (response.ok === true) {
        const ads = await response.json();
        const rows = document.querySelector("tbody");
        ads.forEach(ad => rows.append(row(ad)));
    }
}

// Добавление объявления
async function createAd(name, link, keyWords) {
    const response = await fetch("/api/ads", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({ name: name, link: link, keyWords: keyWords })
    });
    if (response.ok === true) {
        const ad = await response.json();
        document.querySelector("tbody").append(row(ad));
    }
    else {
        const error = await response.json();
        alert(error.message);
    }
}

// Удаление пользователя
async function deleteAd(postID) {
    const response = await fetch(`/api/ads/${postID}`, {
        method: "DELETE",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const ad = await response.json();
        document.querySelector(`tr[data-rowid='${ad.postID}']`).remove();
    }
    else {
        const error = await response.json();
        alert(error.message);
    }
}

// сброс данных формы после отправки
function reset() {
        document.getElementById("name").value =
        document.getElementById("link").value =
        document.getElementById("keyWords").value = "";
}

// создание строки для таблицы
function row(ad) {
    const tr = document.createElement("tr");
    tr.setAttribute("data-rowid", ad.postID);

    const nameTd = document.createElement("td");
    nameTd.append(ad.name);
    tr.append(nameTd);

    const positionTd = document.createElement("td"); //позиция
    positionTd.className = "positionColumn";
    positionTd.append(ad.position != 0 ? ad.position : "?");
    tr.append(positionTd);

    const statisticsButton = document.createElement("p"); 
    statisticsButton.className = "statisticsButton";
    const statisticsButtonA = document.createElement("a");
    statisticsButtonA.append("Статистика");
    statisticsButtonA.href = `/statistics/${ad.postID}`;

    positionTd.appendChild(statisticsButton).appendChild(statisticsButtonA);

    

    const cityTd = document.createElement("td");
    cityTd.append(ad.city);
    tr.append(cityTd);

    const keyWordsTd = document.createElement("td");
    keyWordsTd.append(ad.keyWords);
    tr.append(keyWordsTd);

    const linkTd = document.createElement("td"); //открыть ссылку
    const linkTdAndA = document.createElement("a");
    linkTdAndA.target = "_blank";
    linkTdAndA.innerHTML = "Открыть";
    linkTdAndA.href = ad.link;
    linkTd.append(linkTdAndA);
    tr.append(linkTd);

    const linksTd = document.createElement("td"); //удаление
    const removeLink = document.createElement("button");
    removeLink.className = "removeButton";
    const imageDelete = document.createElement("img");
    imageDelete.className = "deleteImage";
    removeLink.append(imageDelete);
    removeLink.addEventListener("click", async () => await deleteAd(ad.postID));

    linksTd.append(removeLink);
    tr.appendChild(linksTd);

    return tr;
}

// сброс значений формы
document.getElementById("resetBtn").addEventListener("click", () => reset());

// отправка формы
document.getElementById("saveBtn").addEventListener("click", async () => {
    const saveBtn = document.getElementById("saveBtn");
    saveBtn.disabled = true;
    const name = document.getElementById("name").value;
    const link = document.getElementById("link").value;
    const keyWords = document.getElementById("keyWords").value;
    reset();
    try {
        await createAd(name, link, keyWords);
    }
    finally {
        saveBtn.disabled = false;
    }
});


// загрузка объявлений
getAds();

