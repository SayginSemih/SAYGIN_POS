const express = require("express");
const app = express();
const userRouter = require("./router/userRouter");

// TEMPLATE ENGİNE
app.set("view engine", "ejs");

// PUBLİC FOLDER
app.use(express.static("node_modules"));
app.use(express.static("public"));

app.use(userRouter);

app.listen(3000, () => {
    console.log("Sunucu 3000 portundan başlatıldı!");
})