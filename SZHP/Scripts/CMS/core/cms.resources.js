
CMS.namespace("Resources");

CMS.Resources = (function () {
    "use strict";

    var RoleType = {
        None: 0,
    }

    return {
        Messages: {
            Msg_Authen_Credentials: "There was a problem authenticating your credentials: Please try again.",
            Msg_Record_Deleted: "Record Deleted successfully..",
            Msg_Login_Success: "Login is successful, entering into the application...",
            Error: "Error",
            Still_Under_Constrction: "This is under construction..",
            Success: "Success",
            Warning: "Warning",
            Information: "Information",
            Msg_Change_Lang: "Please wait while we change you language....",
            Msg_Record_Save_Successfully: "Record  has been saved successfully..",
            Msg_Data_Save_Success: "Data Has been Saved Successfully",
            Msg_Select_One_Subscription: "Please Select Atleast One Subscription Frequency",
            Msg_Select_One_Record: "Select At least One Record",
            Msg_Confirm_Delete: "Are You Sure You Want to Delete?",
            Msg_Item_Deleted: "Please select the items to delete.",
            Msg_No_Record_Del: "No records were deleted.",
            Msg_Record_Not_Saved: "Record Could Not saved successfully...",
            Msg_Target_Job_Already: "Record Could Not be Save Because this user already have saved Target Job ...",
            Msg_Job_Not_Delete: "Some Job Post could not be Deleted",
            Msg_Hobbies_Updated: "Hobby has been updated successfully..",
            Msg_Update_Success: "Updated successfully..",
            You_must_enter_name: "You must enter a user name",
            You_must_enter_password: "You must enter a password",
            You_must_enter_email_address: "You must enter an email address"

        },

        Views: {
            LoginURL: CMS.Globals.baseURL + "User/Login",
            SchedHomeURL: CMS.Globals.baseURL + "test/home",
        },
        Images: {
            CloseImageURL: CMS.Globals.baseURL + "content/autoComplete/images/close_icon.gif",
            ExpImageURL: CMS.Globals.baseURL + "content/images/img-expand.png",
            ColapseImageURL: CMS.Globals.baseURL + "content/images/img-collapse.png",
            ImageURL: CMS.Globals.baseURL + "Images/IconsAuxCodes/",
            ContentFolderImages: CMS.Globals.baseURL + "Images/Content/images/"
        },
        FilePath: {

        },
        TemplatePath: {

        },
        CurrentUserInfo: {
            LoginId: ''
        }
    };
}());

