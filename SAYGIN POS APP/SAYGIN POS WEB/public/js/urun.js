const btn = document.querySelector("#submitButton");
const form = document.querySelector("#myForm");

btn.addEventListener("click", () => {
    let onay = confirm("Onayla");
    if (onay)
    {
        form.submit();
    }
})