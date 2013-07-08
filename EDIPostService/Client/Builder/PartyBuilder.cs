using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIPostService.Client.Builder
{
    public class PartyBuilder
    {
        private int _id;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _companyName;

        public String companyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }


        private String _customerNumber;

        public String customerNumber
        {
            get { return _customerNumber; }
            set { _customerNumber = value; }
        }


        private String _streetAddress;

        public String streetAddress
        {
            get { return _streetAddress; }
            set { _streetAddress = value; }
        }

        private String _streetZip;

        public String streetZip
        {
            get { return _streetZip; }
            set { _streetZip = value; }
        }


        private String _streetCity;

        public String streetCity
        {
            get { return _streetCity; }
            set { _streetCity = value; }
        }

        private String _postAddress;

        public String postAddress
        {
            get { return _postAddress; }
            set { _postAddress = value; }
        }

        private String _postZip;

        public String postZip
        {
            get { return _postZip; }
            set { _postZip = value; }
        }

        private String _postCity;

        public String postCity
        {
            get { return _postCity; }
            set { _postCity = value; }
        }

        private String _country ;

        public String country
        {
            get { return _country; }
            set { _country = value; }
        }

        private String _contactName;

        public String contactName
        {
            get { return _contactName; }
            set { _contactName = value; }
        }

        private String _contactPhone;

        public String contactPhone
        {
            get { return _contactPhone; }
            set { _contactPhone = value; }
        }

        private String _contactCellphone;

        public String contactCellphone
        {
            get { return _contactCellphone; }
            set { _contactCellphone = value; }
        }

        private String _contactEmail;

        public String contactEmail
        {
            get { return _contactEmail; }
            set { _contactEmail = value; }
        }



    }
}
