async function signIn(name, password) {
    const response = await fetch("/signin", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({ name: name, password: password})
    });
    if (response.ok == true)
        window.location.replace("/monitoring");
    else alert("Введены неверные данные");
}

async function signUp(name, password) {
    const response = await fetch("/signup", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({ name: name, password: password })
    });
    if (response.ok == true)
        window.location.replace("/monitoring");
    else alert("Некорректные данные");
}

document.getElementById("signIn").addEventListener("click", async () => {
    const signInBtn = document.getElementById("signIn");
    signInBtn.disabled = true;
    const name = document.getElementById("name").value;
    const password = document.getElementById("password").value;
    try {
        await signIn(name, password);
    }
    finally {
        signInBtn.disabled = false;
    }
});

document.getElementById("signUp").addEventListener("click", async () => {
    const signUpBtn = document.getElementById("signUp");
    signUpBtn.disabled = true;
    const name = document.getElementById("name").value;
    const password = document.getElementById("password").value;
    try {
        await signUp(name, password);
    }
    finally {
        signUpBtn.disabled = false;
    }
});