using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;

namespace AlphaService
{
    // interface em que os métodos são declarados e lhes são atribuídos endereços http
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebGet(UriTemplate="getallplatoons")]
        XElement GetAllPlatoons();

        [OperationContract]
        [WebGet(UriTemplate = "getplatoonbyid/{platoonid}")]
        XElement GetPlatoonById(string platoonid);

        [OperationContract]
        [WebGet(UriTemplate = "insertplatoon/{name}/{initialdate}/{finaldate}")]
        XElement InsertPlatoon(string name, string initialdate, string finaldate);

        [OperationContract]
        [WebGet(UriTemplate = "updateplatoon/{id}/{name}/{initialdate}/{finaldate}")]
        XElement UpdatePlatoons(string id, string name, string initialdate, string finaldate);

        [OperationContract]
        [WebGet(UriTemplate = "deleteplatoon/{id}")]
        XElement DeletePlatoons(string id);

        [OperationContract]
        [WebGet(UriTemplate = "getallrecruits")]
        XElement GetAllRecruits();

        [OperationContract]
        [WebGet(UriTemplate = "getrecruitbyid/{recruitId}")]
        XElement GetRecruitById(string recruitId);

        [OperationContract]
        [WebGet(UriTemplate = "getallstaff")]
        XElement GetAllStaff();

        [OperationContract]
        [WebGet(UriTemplate = "getstaffbyid/{staffId}")]
        XElement GetStaffById(string staffId);

        [OperationContract]
        [WebGet(UriTemplate = "insertstaff/{name}/{email}/{platoonId}")]
        XElement InsertStaff(string name, string email, string platoonId);

       /* [OperationContract]
        [WebGet(UriTemplate = "insertstaff/{name}/{email}")]
        XElement InsertStaff(string name, string email);*/

        [OperationContract]
        [WebGet(UriTemplate = "updatestaff/{id}/{name}/{email}/{platoonId}")]
        XElement UpdateStaff(string id, string name, string email, string platoonId);

        [OperationContract]
        [WebGet(UriTemplate = "deletestaff/{id}")]
        XElement DeleteStaff(string id);

        [OperationContract]
        [WebGet(UriTemplate = "insertrecruit/{name}/{email}/{platoonId}")]
        XElement InsertRecruit(string name, string email, string platoonId);

        [OperationContract]
        [WebGet(UriTemplate = "updaterecruit/{id}/{name}/{email}/{platoonId}")]
        XElement UpdateRecruit(string id, string name, string email, string platoonId);
    }

}
