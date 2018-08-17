using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRUDGenerator.Database;

namespace CRUDGenerator.Templates.Classes.PHP
{
    public class AbstractDAOGenerator
    {
        public static string getAbstractDAOClass(DatabaseXML database)
        {
            string res = @"<?php
if(!defined('AbstractDAO')){
    define('AbstractDAO','AbstractDAO');
    abstract class AbstractDao{
        protected $db = NULL;
        
        public function __construct(){
            if($this->db == NULL) {
                try{
                    $this->db = new PDO('mysql:host=localhost;dbname=DATABASE;charset=utf8', 'USERNAME', 'PASSWORD');
                }catch (PDOException $e) {
                    echo 'Échec lors de la connexion : ' . $e->getMessage();
                }
            }
        }
    }
}
?>";
            return res;
        }
    }
}