using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
namespace MagicManagerService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IService1
    {
        
        [OperationContract]
        string GetData(int value);

        //[OperationContract]
        //CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: ajoutez vos opérations de service ici

        [OperationContract]
        string AccountRequest();

        //[OperationContract]
        //CompositeType GetAccountUsingDataContract(CompositeType composite);

        [OperationContract]
        string GameRequest();

        //[OperationContract]
        //CompositeType GetGameUsingDataContract(CompositeType composite);

        [OperationContract]
        string ProdutRequest(int id);

        //[OperationContract]
        //CompositeType GetProductUsingDataContract(CompositeType composite);

        [OperationContract]
        string ArticleRequest(int id);

        //[OperationContract]
        //CompositeType GetArticleUsingDataContract(CompositeType composite);

        [OperationContract]
        string ExpansionRequest(int id);

        //[OperationContract]
        //CompositeType GetExpansionUsingDataContract(CompositeType composite);

        [OperationContract]
        string ProductByExpansionRequest(int idGame, string expansionName );

        //[OperationContract]
        //CompositeType GetProductByExpansionUsingDataContract(CompositeType composite);

        [OperationContract]
        string StockRequest();

        //[OperationContract]
        //CompositeType GetStockUsingDataContract(CompositeType composite);

    }


    // Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
    //[DataContract]
    //public class CompositeType
    //{
        
    //    bool boolValue = true;
    //    string stringValue = "Hello ";

    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }

    //    [DataMember]
    //    public string StringValue
    //    {
    //        get { return stringValue; }
    //        set { stringValue = value; }
    //    }
    //}

    [DataContract]
    public class Expansion
    {
        [DataMember]
        public int idExpansion { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int icon { get; set; }
    }

    [DataContract]
    public class Game
    {
        [DataMember]
        public int idGame { get; set; }
        [DataMember]
        public string name { get; set; }
    }

    [DataContract]
    public class Article
    {
        [DataMember]
        public int idArticle { get; set; }
        [DataMember]
        public int idProduct { get; set; }
        [DataMember]
        public Language language { get; set; }
        [DataMember]
        public string comments { get; set; }
        [DataMember]
        public double price { get; set; }
        [DataMember]
        public int count { get; set; }
        [DataMember]
        public bool inShoppingCart { get; set; }
        [DataMember]
        public Seller seller { get; set; }
        [DataMember]
        public string condition { get; set; }
        [DataMember]
        public bool isFoil { get; set; }
        [DataMember]
        public bool isSigned { get; set; }
        [DataMember]
        public bool isPlayset { get; set; }
        [DataMember]
        public bool isAltered { get; set; }
    }

    [DataContract]
    public class Language
    {
        [DataMember]
        public int idLanguage { get; set; }
        [DataMember]
        public string languageName { get; set; }
    }


    public class Seller
    {
        public int idUser { get; set; }
        public string username { get; set; }
        public string country { get; set; }
        public int isCommercial { get; set; }
        public int riskGroup { get; set; }
        public int reputation { get; set; }
        public int shipsFast { get; set; }
        public int sellCount { get; set; }
        public bool onVacation { get; set; }
        public int idDisplayLanguage { get; set; }
    }

}
