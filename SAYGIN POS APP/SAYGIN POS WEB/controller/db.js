const mysql = require("mysql2");

let con = mysql.createConnection({
    host : 'localhost',
    user : 'root',
    password : '',
    database : 'cafeteria'
});

module.exports = con;