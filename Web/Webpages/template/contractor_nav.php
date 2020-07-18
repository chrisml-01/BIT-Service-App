<nav class="navbar navbar-expand-lg backgroundSidebar border-bottom">
        <button class="btn btn-primary" id="menu-toggle">Menu</button>

        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <ul class="navbar-nav ml-auto mt-2 mt-lg-0"> 
            <li class="nav-item active">
              <a class="navlink" href="submitJob.php">Submit for Approval <span class="sr-only">(current)</span></a>
            </li>           
            <li class="nav-item dropdown">
              <a class="navlink dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <?php 
                      $db = mysqli_connect('localhost', 'root', '', 'bit_luntok'); 
                      $queryStrContractor = "select FirstName from contractor where Contractorid =". $_SESSION["Contractorid"];
                      $result = mysqli_query($db, $queryStrContractor);
                      $row = mysqli_fetch_array($result);  
                      $name =  $row[0];                 
                      echo "Welcome back, " .  $name ;                    
                ?>
              </a>
              <div class="dropdown-menu dropdown-menu-right" aria-labelledby="navbarDropdown">
                <!-- <a class="dropdown-item" href="#">Settings</a>
                <div class="dropdown-divider"></div> -->
                <a class="dropdown-item" href="logout.php">Logout</a>
              </div>
            </li>
          </ul>
        </div>
      </nav>