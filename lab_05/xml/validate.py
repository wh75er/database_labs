from lxml import etree
import sys

def validate(fxml, fxsd):
    schema_root = etree.XML(fxsd.read())
    schema = etree.XMLSchema(schema_root)
    parser = etree.XMLParser(schema = schema)
    
    result = True
    try:
        etree.fromstring(fxml.read(), parser);
    except:
        result = False
    return result

def open_files(names):
    xsd_name, xml_name = names
    fxsd = open(str(xsd_name), 'r');
    fxml = open(str(xml_name), 'r');
    return  fxsd, fxml

def get_file_names():
    args_count = len(sys.argv)
    if args_count < 3 or args_count > 4:
        raise Exception('incorrect amount of args! You entered : {}, but you need 2'.format(args_count-1))

    return sys.argv[1], sys.argv[2]


if __name__=="__main__":
    fxsd, fxml = open_files(get_file_names())

    print("Validation of file {} with xsd {} is {}".format(
                str(sys.argv[2]), str(sys.argv[1]), validate(fxml, fxsd)))

    fxsd.close()
    fxml.close()
