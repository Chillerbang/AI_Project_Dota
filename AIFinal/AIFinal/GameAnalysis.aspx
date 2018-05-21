<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="GameAnalysis.aspx.cs" Inherits="AIFinal.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .Padd{
            padding-top:5%;
            padding-left:15%;
        }
    </style>
     
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        
    <div class="Padd text-Left" id="Display">
        <h3>Major emotion experienced in game</h3>
        <div class="row table-dark">
        <asp:Table ID="tblEmotions" Class="col-12" runat="server"></asp:Table>
        </div>
        <br/>

        <h3> Emotion at Game events </h3>
        <div class="row table-dark">
        <asp:Table ID="tblGameEvents" Class="col-12" runat="server"></asp:Table>
        </div>
        <br/>

        <h3> Words and thier emotion </h3>
        <div class="row table-dark">
        <asp:Table ID="tblEmotionWord" Class="col-12" runat="server"></asp:Table>
        </div>
        <br/>

        <h3> Most effective words </h3>
        <div class="row table-dark">
        <asp:Table ID="tblPredictedWords" Class="col-12" runat="server"></asp:Table>
        </div>
    </div>
</asp:Content>
