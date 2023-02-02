db = new Mongo().getDB("bibleTextDB");
db.createCollection('Book', { capped: false });