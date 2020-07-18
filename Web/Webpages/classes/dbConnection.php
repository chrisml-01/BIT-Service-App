<?php
    class Connection{

        private $db;

                public function __construct(){
                    $this->db = mysqli_connect('localhost', 'root', '','bitver3');
                }

                public function executeQuery($sqlString){
                   return mysqli_query($this->db,$sqlString);
                }
    }

?>