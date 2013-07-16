using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDIPostService.Client;

namespace EDIPostService
{
    interface iEDIPostService
    {

        /// <summary>
        /// Fetches a default consignor
        /// </summary>
        /// <returns>Consignor</returns>
        Consignor getDefaultConsignor();


        /// <summary>
        /// Creates a consignee party ( recipient )
        /// </summary>
        /// <param name="consignee"></param>
        /// <returns>Consignee</returns>
        Consignee createConsignee(Consignee consignee);

        /// <summary>
        /// Find all available products for Consignment
        /// </summary>
        /// <param name="c"></param>
        /// <returns>Consignment</returns>
        List<Product> findProducts(Consignment c);


        /// <summary>
        /// Find the postage for the current consignment
        /// </summary>
        /// <param name="c"></param>
        /// <returns>Consignment</returns>
        Consignment getPostage(Consignment c);

        /// <summary>
        /// Prints the consignment and return a PDF stream
        /// </summary>
        /// <param name="c"></param>
        /// <returns>a stream containing the PDF</returns>
        string printConsignment(Consignment c);

        /// <summary>
        /// Fetches a consignment from the archive
        /// </summary>
        /// <param name="id">id of the consignment to get</param>
        /// <returns>consignment</returns>
        Consignment getConsignment(int id);


        /// <summary>
        /// Create the consignment in EDIPost
        /// </summary>
        /// <param name="c">The consignment to create</param>
        /// <returns>the created consignment</returns>
        Consignment createConsignment(Consignment c);

        /// <summary>
        /// Finds consignements from the archive based on a search criterea
        /// </summary>
        /// <param name="query"></param>
        /// <returns>List of consignments</returns>
        List<Consignment> searchConsignment(string query);

        /// <summary>
        /// Finds consignee from the EDIPost addressbook
        /// </summary>
        /// <param name="query">the search query</param>
        /// <returns>List of consigees</returns>
        List<Consignee> searchConsignee(string query);

        /// <summary>
        /// Fetches a consignee on ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the consignee</returns>
        Consignee getConsignee(int id);

    }
}
