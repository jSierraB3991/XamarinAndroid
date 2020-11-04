namespace SqlLiteAndroid.Data
{
    using Android.Util;
    using SQLite;
    using SqlLiteAndroid.Model;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class DatabaseLite
    {
        private readonly string _folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public bool CreateDatabase()
        {
            try
            {
                using (var con = new SQLiteConnection(Path.Combine(_folder, "xamSqliteItem.db")))
                {
                    con.CreateTable<Item>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqlException", ex.Message);
                return false;
            }
        }

        public Item InserItem(Item item)
        {
            try
            {
                using (var con = new SQLiteConnection(Path.Combine(_folder, "xamSqliteItem.db")))
                {
                    con.Insert(item);
                    return item;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqlException", ex.Message);
                return null;
            }
        }

        public List<Item> SelectTableItems()
        {
            try
            {
                using (var con = new SQLiteConnection(Path.Combine(_folder, "xamSqliteItem.db")))
                {
                    return con.Table<Item>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqlException", ex.Message);
                return null;
            }
        }

        public Item SelectTableItemById(int id)
        {
            try
            {
                using (var con = new SQLiteConnection(Path.Combine(_folder, "xamSqliteItem.db")))
                {
                    return con.Table<Item>().FirstOrDefault(i => i.Id.Equals(id));
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqlException", ex.Message);
                return null;
            }
        }

        public bool UpdateItem(Item item)
        {
            try
            {
                using (var con = new SQLiteConnection(Path.Combine(_folder, "xamSqliteItem.db")))
                {
                    con.Query<Item>("UPDATE Item Set Name = ?, Age = ?, Email = ? WHERE id = ?",
                        item.Name, item.Age, item.Email, item.Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqlException", ex.Message);
                return false;
            }
        }

        public bool DeleteIem(Item item)
        {
            try
            {
                using (var con = new SQLiteConnection(Path.Combine(_folder, "xamSqliteItem.db")))
                {
                    con.Delete(item);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqlException", ex.Message);
                return false;
            }
        }

        public bool ExistsItem(int id) => this.SelectTableItemById(id) != null;

        public void DeleteItems()
        {
            try
            {
                using (var con = new SQLiteConnection(Path.Combine(_folder, "xamSqliteItem.db")))
                {
                    con.DropTable<Item>();
                    con.CreateTable<Item>();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqlException", ex.Message);
            }
        }
    }
}