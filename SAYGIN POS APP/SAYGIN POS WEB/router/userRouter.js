const express = require("express");
const db = require("../controller/db.js")
const router = express.Router();
var bodyParser = require('body-parser');
 
// POST islemini cozumlemek icin gerekli bir ayristirici
var urlencodedParser = bodyParser.urlencoded({ extended: false })

router.get("/:id/siparis/:urunid", (req, res) => {
    var ID = req.params.id;
    var urunid = req.params.urunid
    try{
        db.query("SELECT * FROM orders WHERE id=?", [urunid], (err, query) => {
            res.render(`sil`, {
                ID,
                query: query[0]
            });

        })
    }
    catch (ex){
        console.log(ex);
    }
});

router.post("/:id", urlencodedParser,(req, res) => {
    var ID = req.params.id;
    var not = req.body.not;
    try{
        db.query("SELECT * FROM product WHERE id=?", [req.body.urun], (err, query) => {
            var order = {
                urun: query[0].urun,
                fiyat: query[0].fiyat,
                category: query[0].category,
                masa: req.params.id,
                ip: req.ip,
                musterinotu: not,
                durum: "BEKLEMEDE"
            };
            db.query("INSERT INTO orders SET ?", order, (err, results) => {
                if (err) {
                  console.error('Veri eklenirken hata oluştu: ' + err.message);
                  return;
                }
                console.log('Veri başarıyla eklendi.');
                try{
                    db.query("SELECT * FROM orders WHERE masa=?", [ID], (err, query) => {
                        if (query.length>0)
                        {
                            res.render("index", {
                                ID,
                                query
                            });
                        }
                        else
                        {
                            res.render("index", {
                                ID,
                                query: false
                            });
                        }
                    })
                }
                catch (ex){
                    console.log(ex);
                }
              });
        })
    }
    catch (ex){
        console.log(ex);
    }
});

router.get("/:id/:catagory/:product", (req, res) => {
    var ID = req.params.id;
    var PT = req.params.product;
    try{
        db.query("SELECT * FROM product WHERE id=?", [PT], (err, query) => {
            res.render(`urun`, {
                ID,
                query
            });

        })
    }
    catch (ex){
        console.log(ex);
    }
});

router.get("/:id/:catagory", (req, res) => {
    var ID = req.params.id;
    var CT = req.params.catagory;
    if(req.params.catagory<5001 || req.params.catagory>5018)
    {
        res.send("<h1>Sayfa Bulunamadı!</h1>");
    }
    else
    {
        try{
            db.query("SELECT * FROM product WHERE category=?", [CT], (err, query) => {
                for(let i=5001; i<=5018;i ++)
                {
                    if (req.params.catagory==i)
                    {
                        res.render(`${i}`, {
                            i,
                            ID,
                            query
                        });
                    }
                }
            })
        }
        catch (ex){
            console.log(ex);
        }
    }
});

router.get("/:id", (req, res) => {
    const ID = req.params.id;
    try{
        db.query("SELECT * FROM orders WHERE masa=?", [ID], (err, query) => {
            if (query.length>0)
            {
                res.render("index", {
                    ID,
                    query
                });
            }
            else
            {
                res.render("index", {
                    ID,
                    query: false
                });
            }
        })
    }
    catch (ex){
        console.log(ex);
    }
});

router.post("/:id/iptal", urlencodedParser, (req, res) => {
    ID = req.params.id
    var urun = req.body.urun;
    try{
        db.query("DELETE FROM orders WHERE id=?", [urun], (err, query) => {
            res.render(`iptal`, {
                ID
            });
        })
    }
    catch (ex){
        console.log(ex);
    }
});

module.exports = router;