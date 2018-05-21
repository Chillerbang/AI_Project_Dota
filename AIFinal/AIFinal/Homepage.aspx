<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="AIFinal.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //var circle = document.getElementById("CircleWait");
        //circle.style.display = "none";
        function hideMain() {
            var mainDiv = document.getElementById("Main");
            mainDiv.style.display = "none";
            var circle = document.getElementById("CircleWait");
            circle.style.display = "block";
        };
    </script>
    <link href="css/animatedWait.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <hr><hr><hr>
    <div ID="CircleWait" style="display:none;" class="CircleWait"></div>

   <div  class="container" id="Main">

       <div class="row align-self-start">
            <div  class="col-3"> 
            </div >
       <div  class="col-8">
          <div class="card-header">Add a new Game and recording</div>
          <div class="card-body">
              <div class="form-group">
                <label for="GameIDInput">GameID</label>
                <input class="form-control" id="GameIDInput" type="text" aria-describedby="upload audio" runat="server" placeholder="Enter dota 2 gameID">
              </div>
            
              <div class="form-group">
                <label for="UploadAudioFile">MP3 audio file</label><br />
               
                <div class="row">
                    <div class="col-12">
                        <asp:FileUpload Class="form-control" ID="uploadAudioFile" runat="server" />
                    </div>
                </div>
              </div>
              </div>
              <asp:Button ID="btnAddGame" Class="btn btn-success btn-block" runat="server" OnClick="btnAddGame_Click" Text="Add new game"  OnClientClick="hideMain()"/>
           <label id="test" runat="server"></label>
            </div>
          </div>
        </div>

</asp:Content>
