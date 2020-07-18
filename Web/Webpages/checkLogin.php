<html>
    <head>
    </head>
    <body>
    <?php
    	$db = mysqli_connect('localhost', 'root', '', 'bit_luntok');
        $username = $_POST['uname'];
        $password = $_POST['psw'];

        if($username == "" || $password == ""){
            ?>
                <script>
                    alert('Please make sure that your username or password is not empty.');
                    window.location = 'login.php';
                </script>
            <?php  
        }else{
            
            $sqlClient = "select Clientid, UserName, Password from Client where Username = '" .$username . "' and password = '" .$password . "'";
            $result = mysqli_query($db, $sqlClient);
    
            $sqlContractor = "select Contractorid, UserName, Password from Contractor where Username = '" .$username . "' and password = '" .$password . "'";
            $result1 = mysqli_query($db, $sqlContractor);
    
            $sqlCoord = "select Coordinatorid, UserName, Password from Coordinator where Username = '" .$username . "' and password = '" .$password . "'";
            $result2 = mysqli_query($db, $sqlCoord);
            
            
            $row = mysqli_fetch_array($result);
            $row1 = mysqli_fetch_array($result1);
            $row2 = mysqli_fetch_array($result2);
    
            session_start();
    
            if(mysqli_num_rows($result1) == 1){
    
                $_SESSION["Contractorid"] = $row1[0];            
                header("location: ContractorManagement.php");
                $_SESSION["Type"] = "Contractor";            
            } else if(mysqli_num_rows($result2) == 1) {
                $_SESSION["Coordinatorid"] = $row2[0];
                $_SESSION["Type"] = "Coordinator";            
                header("location: coordinatorManagement.php");
            } else if(mysqli_num_rows($result) == 1) {
     
                $_SESSION["Clientid"] = $row[0];    
                $_SESSION["Type"] = "Client";        
                header("location: ClientManagement1.php");
            } else {
                // echo("<script>alert('username or password incorrect!')</script>");
                // header("location: login.php");
                ?>
                    <script>
                        alert('Incorrect username or password! Please try again');
                        window.location = 'login.php';
                    </script>
                <?php    
                // echo("username or password incorrect!");
            }
        }
        

      

    ?>
    </body>
</html>