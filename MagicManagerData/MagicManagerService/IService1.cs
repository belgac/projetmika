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
        IEnumerable<GameMkm> GameRequest();

        //[OperationContract]
        //CompositeType GetGameUsingDataContract(CompositeType composite);

        [OperationContract]
        ProductMkm ProductRequest(int id);

        //[OperationContract]
        //CompositeType GetProductUsingDataContract(CompositeType composite);

        [OperationContract]
        ArticleMkm ArticleRequest(int id);

        //[OperationContract]
        //CompositeType GetArticleUsingDataContract(CompositeType composite);

        [OperationContract]
        List<ExpansionMkm> ExpansionRequest(int id);

        //[OperationContract]
        //CompositeType GetExpansionUsingDataContract(CompositeType composite);

        [OperationContract]
        IEnumerable<ProductMkm> ProductInExpansionRequest(int idGame, string expansionName );

        //[OperationContract]
        //CompositeType GetProductByExpansionUsingDataContract(CompositeType composite);

        [OperationContract]
        IEnumerable<ArticleMkm> StockRequest();

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
    public class ExpansionMkm
    {
        [DataMember]
        public int idExpansion { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int icon { get; set; }
    }

    [DataContract]
    public class GameMkm
    {
        [DataMember]
        public int idGame { get; set; }
        [DataMember]
        public string name { get; set; }
    }

    [DataContract]
    public class ArticleMkm
    {
        [DataMember]
        public int idArticle { get; set; }
        [DataMember]
        public int idProduct { get; set; }
        [DataMember]
        public LanguageMkm language { get; set; }
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
        public bool isFirstEd { get; set; }
        [DataMember]
        public bool isSigned { get; set; }
        [DataMember]
        public bool isPlayset { get; set; }
        [DataMember]
        public bool isAltered { get; set; }
        [DataMember]
        public string rarity { get; set; }
        [DataMember]
        public PriceGuide priceGuide { get; set; }
        [DataMember]
        public DateTime lastEdited { get; set; }

    }

    [DataContract]
    public class LanguageMkm
    {
        [DataMember]
        public int idLanguage { get; set; }
        [DataMember]
        public string languageName { get; set; }
    }


    [DataContract]
    public class Lang
    {
        [DataMember]
        public int idLanguage { get; set; }
        [DataMember]
        public string languageName { get; set; }
        [DataMember]
        public string productName { get; set; }
    }

    [DataContract]
    public class Category
    {
        [DataMember]
        public long idCategory { get; set; }
        [DataMember]
        public string categoryName { get; set; }
    }

    [DataContract]
    public class PriceGuide
    {
        [DataMember]
        public double SELL { get; set; }
        [DataMember]
        public double LOW { get; set; }
        [DataMember]
        public double LOWEX { get; set; }
        [DataMember]
        public double LOWFOIL { get; set; }
        [DataMember]
        public double AVG { get; set; }
        [DataMember]
        public double TREND { get; set; }
    }

    [DataContract]
    public class ProductMkm : Card
    {
        [DataMember]
        public bool inStock { get; set; }
        [DataMember]
        public double myPrice { get; set; }
        [DataMember]
        public string rarity { get; set; }
        [DataMember]
        public int countArticles { get; set; }
        [DataMember]
        public int countFoils { get; set; }
    }

    public class Card
    {
        public int idProduct { get; set; }
        public int idMetaproduct { get; set; }
        public int idGame { get; set; }
        public string countReprints { get; set; }
        public Name name { get; set; }
        public string website { get; set; }
        public string image { get; set; }
        public Category category { get; set; }
        public string expansion { get; set; }
        public int expIcon { get; set; }
        public object number { get; set; }
    }

    public class Name
    {
        // public Lang lang { get; set; }
        public long idLanguage { get; set; }
        public string languageName { get; set; }
        public string productName { get; set; }
    }

    [DataContract]
    public class RootArticle
    {
        public List<ArticleMkm> article { get; set; }
    }

    [DataContract]
    public class RootLanguage
    {
        public List<LanguageMkm> language { get; set; }
    }

    [DataContract]
    public class RootGame
    {
        public List<GameMkm> game { get; set; }
    }

    [DataContract]
    public class RootExpansion
    {
        public List<ExpansionMkm> expansion { get; set; }
    }

    [DataContract]
    public class RootProduct
    {
        public List<ProductMkm> product { get; set; }
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
