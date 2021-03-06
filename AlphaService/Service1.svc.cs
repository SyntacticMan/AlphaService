﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;

namespace AlphaService
{
    
    public class Service1 : IService1
    {
        // inicializar a base de dados
        AlphaDBP12Entities1 db = new AlphaDBP12Entities1();

        public XElement GetAllRecruits()
        {
            // converter a base de dados para uma lista
            List<Recruits> recruitsList = db.Recruits.ToList();

            // inicializar elemento XML root
            XElement xMainElem = new XElement("recruits");

            // percorrer os dados dos recrutas e enfiá-los no xml

            foreach (Recruits item in recruitsList)
            {
                XElement xElem = new XElement("recruit",
                    new XElement("id", item.Id),
                    new XElement("name", item.Name),
                    new XElement("email", item.Email),
                    new XElement("platoonid", item.PlatoonId)
                    );

                // acrescentar o elemento ao documento xml
                xMainElem.Add(xElem);
            }

            return xMainElem;
        }

        public XElement GetRecruitById(string recruitId)
        {
            // criar um recruta que vai ser recuperado
            Recruits recruit = new Recruits();

            // convert id para int32
            int int_RecruitId = Convert.ToInt32(recruitId);

            // validar o ID e recuperar o recruta da base de dados
            if (!string.IsNullOrEmpty(recruitId))
            {
                recruit = db.Recruits.Where(x => x.Id.Equals(int_RecruitId)).FirstOrDefault();
            }
            else
            {
                return new XElement("");
            }

            // criar o elemento xml para devolver o recruta

            XElement xElem = new XElement("recruit",
                new XElement("id", recruit.Id),
                new XElement("name", recruit.Name),
                new XElement("email", recruit.Email),
                new XElement("platoonid", recruit.PlatoonId)
                );

            return xElem;
        }

        /// <summary>
        /// método para inserir um recruta na base de dados
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="platoonId"></param>
        /// <returns></returns>
        public XElement InsertRecruit(string name, string email, string platoonId)
        {
            XElement xElem = new XElement("isInserted");

            // validar parâmetros e dar entrada do registo
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(platoonId))
            {
                Recruits recruit = new Recruits { Name = name, Email = email, PlatoonId =  Convert.ToInt32(platoonId) };
                db.Recruits.Add(recruit);
                db.SaveChanges();
                xElem.Value = "true";
                return xElem;
            }

            xElem.Value = "false";
            return xElem;
        }

        /// <summary>
        /// método para actualizar o recruta
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="platoonId"></param>
        /// <returns></returns>
        public XElement UpdateRecruit(string id, string name, string email, string platoonId)
        {
            XElement xElem = new XElement("isUpdated");

            // validar os parâmetros e actualizar o registo
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(platoonId))
            {
                int recruitId = Convert.ToInt32(id);

                Recruits recruit = db.Recruits.Where(x => x.Id.Equals(recruitId)).FirstOrDefault();

                recruit.Name = name;
                recruit.Email = email;
                recruit.PlatoonId = Convert.ToInt32(platoonId);

                db.Entry(recruit).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                xElem.Value = "true";
                return xElem;
            }

            xElem.Value = "false";
            return xElem;
        }

        /// <summary>
        ///  método para remover um recruta da base de dados
        ///  TODO: lidar com possíveis dependências antes de apagar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public XElement DeleteRecruit(string id)
        {
            XElement xElem = new XElement("isDeleted");

            if (!string.IsNullOrEmpty(id))
            {
                int recruitId = Convert.ToInt32(id);

                Recruits recruit = db.Recruits.Where(x => x.Id.Equals(recruitId)).FirstOrDefault();
                db.Recruits.Remove(recruit);
                db.SaveChanges();
                xElem.Value = "true";
                return xElem;
            }

            xElem.Value = "false";
            return xElem;
        }

        /// <summary>
        ///  transforma a lista completa do staff num xml
        /// </summary>
        /// <returns></returns>
        public XElement GetAllStaff()
        {
            List<Staff> staffList = db.Staff.ToList();

            XElement xMainElem = new XElement("staffs");

            foreach (Staff item in staffList)
            {
                XElement xElem = new XElement("staff",
                    new XElement("id", item.Id),
                    new XElement("name", item.Name),
                    new XElement("email", item.Email),
                    new XElement("platoonid", item.PlatoonId)
                    );

                xMainElem.Add(xElem);
            }
            
            return xMainElem;
        }

        /// <summary>
        ///  recupera o elemento do staff por id e devolve um xml
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public XElement GetStaffById(string staffId)
        {
            Staff staff = new Staff();

            int int_StaffId = Convert.ToInt32(staffId);

            if (!string.IsNullOrEmpty(staffId))
            {
                staff = db.Staff.Where(x => x.Id.Equals(int_StaffId)).FirstOrDefault();
            }
            else
            {
                return new XElement("");
            }

            XElement xElem = new XElement("staff",
                new XElement("id", staff.Id),
                new XElement("name", staff.Name),
                new XElement("email", staff.Email),
                new XElement("platoonid", staff.PlatoonId)
                );

            return xElem;
        }

        /// <summary>
        ///  insere um membro do staff na base de dados, requer platoonid apesar de esse elemento não ser necessário
        ///  para ser corrigido eventualmente
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="platoonId"></param>
        /// <returns></returns>
        public XElement InsertStaff(string name, string email, string platoonId)
        {
            XElement xElem = new XElement("isSaved");

            // validar parâmetros de entrada antes de os enfiar na base de dados
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(platoonId))
            {
                Staff staff = new Staff { Name = name, Email = email, PlatoonId = Convert.ToInt32(platoonId) };
                db.Staff.Add(staff);
                db.SaveChanges();
                xElem.Value = "true";
                
            }
            else if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email))
            {
                Staff staff = new Staff { Name = name, Email = email, PlatoonId = null};
                db.Staff.Add(staff);
                db.SaveChanges();
                xElem.Value = "true";
            }
            else
            {
                xElem.Value = "false";
            }
            return xElem;
        }

        /// <summary>
        ///  método para actualizar informação de um membro do staff
        ///  como não sei se dá para enviar um null ou omitir um parâmetro o platoonId continua a ser requirido
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="platoonId"></param>
        /// <returns></returns>
        public XElement UpdateStaff(string id, string name, string email, string platoonId)
        {
            XElement xElem = new XElement("isUpdated");

            // validar parâmetros, recuperar o registo e editá-lo
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(platoonId))
            {
                // converter o id do registo para um int
                int staffId = Convert.ToInt32(id);

                // recuperar o registo
                Staff staff = db.Staff.Where(x => x.Id.Equals(staffId)).FirstOrDefault();

                // actualizar os registos
                staff.Name = name;
                staff.Email = email;
                staff.PlatoonId = Convert.ToInt32(platoonId);

                db.Entry(staff).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                xElem.Value = "true";

                return xElem;
            }

            xElem.Value = "false";
            return xElem;
        }

        /// <summary>
        ///  método para remover um membro do staff
        ///  TODO: validar relações e removê-las antes de apagar o registo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public XElement DeleteStaff(string id)
        {
            XElement xElem = new XElement("isDeleted");

            // validar id e remover o membro do staff
            if (!string.IsNullOrEmpty(id))
            {
                int staffId = Convert.ToInt32(id);

                Staff staff = db.Staff.Where(x => x.Id.Equals(staffId)).FirstOrDefault();
                db.Staff.Remove(staff);
                db.SaveChanges();
                xElem.Value = "true";
                return xElem;
            }

            xElem.Value = "false";
            return xElem;
        }

        // método para obter todos os pelotões
        public XElement GetAllPlatoons()
        {
            // converter a base de dados para uma lista
            List<Platoons> platoonsList = db.Platoons.ToList();

            // inicializar um elemento xml para devolver os dados à página
            XElement xMainElem = new XElement("platoons");

            // percorrer os dados do pelotão e enfiá-los num xml
            foreach (Platoons item in platoonsList)
            {
                XElement xElem = new XElement("platoon",
                    new XElement("id", item.Id),
                    new XElement("name", item.Name),
                    new XElement("initialdate", item.InitialDate.ToShortDateString()),
                    new XElement("finaldate", item.FinalDate.ToShortDateString()));

                xMainElem.Add(xElem);
            }

            return xMainElem;
        }

        // obter um único pelotão, baseado no id e devolvê-lo
        public XElement GetPlatoonById(string platoonid)
        {
            // criar um pelotão
            Platoons platoon = new Platoons();

            // converter o id para int32, ele vem como string
            int int_PlatoonId = Convert.ToInt32(platoonid);

            // validar o id e obter o pelotão da base de dados
            if (!string.IsNullOrEmpty(platoonid))
            {
                platoon = db.Platoons.Where(x => x.Id.Equals(int_PlatoonId)).FirstOrDefault();
            }
            else
            {
                return new XElement("");
            }

            // criar um elemento xml para devolver o pelotão
            XElement xElem = new XElement("platoon",
                    new XElement("id", platoon.Id),
                    new XElement("name", platoon.Name),
                    new XElement("initialdate", platoon.InitialDate.ToShortDateString()),
                    new XElement("finaldate", platoon.FinalDate.ToShortDateString()));

            return xElem;
        }

        // método para inserir um pelotão na base de dados
        public XElement InsertPlatoon(string name, string initialdate, string finaldate)
        {
            // elemento xml para confirmar o sucesso da operação
            XElement xElem = new XElement("isSaved");

            // validar parâmetros de entrada, enfiá-los no objecto platoon e acrescentá-los à base de dados
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(initialdate) && !string.IsNullOrEmpty(finaldate))
            {
                
                Platoons platoon = new Platoons { Name = name, InitialDate = Convert.ToDateTime(initialdate), FinalDate = Convert.ToDateTime(finaldate)};
                db.Platoons.Add(platoon);
                db.SaveChanges();
                xElem.Value = "true";
            }
            else
            {
                xElem.Value = "false";
            }
            return xElem;
        }


        // método para actualizar o pelotão
        public XElement UpdatePlatoons(string id, string name, string initialdate, string finaldate)
        {
            // elemento xml para confirmar sucesso da operação
            XElement xElem = new XElement("isUpdated");

            // validar os parâmetros, recuperar o pelotão e enviar os novos parãmetros para a base de dados
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(initialdate) && !string.IsNullOrEmpty(finaldate))
            {
                int platoonId = Convert.ToInt32(id);
                Platoons platoon = db.Platoons.Where(x => x.Id.Equals(platoonId)).FirstOrDefault();
                platoon.Name = name;
                platoon.InitialDate = Convert.ToDateTime(initialdate);
                platoon.FinalDate = Convert.ToDateTime(finaldate);

                db.Entry(platoon).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                xElem.Value = "true";
                return xElem;
            }

            xElem.Value = "false";
            return xElem;
        }

        // método para apagar um pelotão da base de dados
        public XElement DeletePlatoons(string id)
        {
            // elemento xml para confirmar sucesso da operação
            XElement xElem = new XElement("isDeleted");

            // validar id e remover o pelotão segundo o id indicado
            if (!string.IsNullOrEmpty(id))
            {
                int platoonId = Convert.ToInt32(id);
                Platoons platoon = db.Platoons.Where(x => x.Id.Equals(platoonId)).FirstOrDefault();
                db.Platoons.Remove(platoon);
                db.SaveChanges();
                xElem.Value = "true";
                return xElem;
            }
            xElem.Value = "false";
            return xElem;
        }
    }
}
