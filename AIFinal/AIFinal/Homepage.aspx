<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="AIFinal.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <hr><hr><hr>
   <div  class="container">

       <div class="row align-self-start">
            <div  class="col-3"> 
            </div >
       <div  class="col-8">
          <div class="card-header">Add a new Game and recording</div>
          <div class="card-body">
              <div class="form-group">
                <label for="GameIDInput">GameID</label>
                <input class="form-control" id="GameIDInput" type="text" aria-describedby="upload audio" placeholder="Enter dota 2 gameID">
              </div>
            
              <div class="form-group">
                <label for="UploadAudioFile">Audio file</label><br />
               <!-- <input class="form-control" id="AudioFile" type="" aria-describedby="upload audio" placeholder="Upload audio file"> -->
                <div class="row">
                    <div class="col-6">
                        <asp:FileUpload Class="form-control" ID="UploadAudioFile" runat="server" />
                    </div>
                    <div class="col-6">
                        <asp:Button ID="btnuploadRecording" Class="btn btn-primary btn-block" runat="server" Text="Add audio recording" />
                    </div>
                </div>
              </div>
            <div class="form-group">
                <label for="uploadJSON">Add Json</label><br />
               <!-- <input class="form-control" id="AudioFile" type="" aria-describedby="upload audio" placeholder="Upload audio file"> -->
                <div class="row">
                    <div class="col-6">
                        <asp:FileUpload Class="form-control" ID="uploadJSON" runat="server" />
                    </div>
                    <div class="col-6">
                        <asp:Button ID="btnUploadJson" Class="btn btn-primary btn-block" runat="server" Text="Add Json" />
                    </div>
                </div>
              </div>
                <div class="form-group">
                <label for="audioDate">Data and Time of recording</label>
                    <div class="row">
                        <div class="col-6">
                            <input class="form-control" id="audioDate" type="date" aria-describedby="Date of audio">
                        </div>
                        <div class="col-6">
                            <input class="form-control" id="audioTime" type="time" aria-describedby="time of audio">
                        </div>
                    </div>
                </div>
              </div>
              <asp:Button ID="btnAddGame" Class="btn btn-success btn-block" runat="server" Text="Add new game" />
            </div>
          </div>
        </div>

</asp:Content>
