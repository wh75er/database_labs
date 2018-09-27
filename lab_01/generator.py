import random as rd
import time

path_to_lens = "lens_data"
path_to_cameraBody = "cameraBody_data"
path_to_filter = "filter_data"
path_to_cameraBuild = "cameraBuild_data"


file_lens = open(path_to_lens, 'w')
file_filter = open(path_to_filter, 'w')
file_camBody = open(path_to_cameraBody, 'w')
file_camBuild = open(path_to_cameraBuild, 'w')

base_model_name = ['Kwanon', 'Yami', 'Hansa', 'Carton', 'EOE', 'YELI', 'MI',
                    'A12', 'Beut', 'tratu', 'zzz']
base_brand_name = ['Sony', 'Canon', 
                    'Thomson',
                    'AgfaPhoto',
                    'C-Lux', 'D-Lux', 'V-Lux',
                    'Medion',
                    'Minox',
                    'Praktica',
                    'Rollei',
                    'Tevion',
                    'Traveler',
                    'Vageeswari',
                    'Canon',
                    'Casio',
                    'Epson',
                    'Fujifilm',
                    'Nikon',
                    'Olympus',
                    'Ricoh',
                    'Panasonic',
                    'Pentax',
                    'Sigma']
base_color_name = ['black',
                    'brown',
                    'white',
                    'blue',
                    'yellow',
                    'gray',
                    'red',
                    'silver',
                    'chroma',
                    'purple',
                    'pink']
base_purpose_lens = ['macro',
                        'standart',
                        'telephoto',
                        'wide angle',
                        'portrait']
base_purpose_filter = ['polarizers',
                        'neutral density',
                        'UV / Haze',
                        'Linear']


# -------------------------------------------------------------------------------------------

def get_model():
    model = ""
    model += base_model_name[rd.randint(0, len(base_model_name)-1)]
    flag = False
    if(rd.randint(0, 1)):
        flag = True
        model += ' '+str(chr(rd.randint(97,122)))
        if(rd.randint(0, 1)):
            model += str(chr(rd.randint(48, 57)))
    if(rd.randint(0, 1)):
        if not flag:
            model += ' '
        model += str(chr(rd.randint(65, 90)))
        if(rd.randint(0, 1)):
            model += str(chr(rd.randint(48, 57)))*rd.randint(0, 3)+str(chr(rd.randint(48, 57)))*rd.randint(0, 2)
    return model

def get_brand():
    brand = ""
    brand += base_brand_name[rd.randint(0, len(base_brand_name)-1)]
    return brand

def get_mount():
    mount = ""
    mount += str(chr(rd.randint(65, 90)))
    if(rd.randint(0,1)):
        mount += str(chr(rd.randint(65, 90)))
        if(rd.randint(0,1)):
            mount += str(chr(rd.randint(65, 90)))
    mount += str(chr(rd.randint(48, 57)))*rd.randint(0, 2)+str(chr(rd.randint(48, 57)))
    return mount

def get_megapixels():
    mp = ""
    mp += str(chr(rd.randint(49, 52)))+str(chr(rd.randint(48, 57)))
    return mp

def get_color():
    color = ""
    color += base_color_name[rd.randint(0, len(base_color_name)-1)]
    return color

def gen_cameraBody(number_of_rows, f):
    for i in range(number_of_rows):
        print(str(i+1) + '|' + get_brand() + '|' + get_model() + '|' + get_mount() + '|' + get_megapixels() + '|' + get_color(), file=f)

# -------------------------------------------------------------------------------------------

def get_lens_name():
    name = ""
    name += base_brand_name[rd.randint(0, len(base_brand_name)-1)] + ' ' + \
            str(chr(rd.randint(65, 90)))+str(chr(rd.randint(65, 90)))
    if(rd.randint(0,1)):
        name += str(chr(rd.randint(65, 90)))
    return name

def get_purpose_lens():
    purpose = ""
    purpose += base_purpose_lens[rd.randint(0, len(base_purpose_lens)-1)]
    return purpose

def get_diameter():
    diameter = ""
    diameter += str(chr(rd.randint(52, 57)))+str(chr(rd.randint(48, 57)))
    return diameter

def gen_lens(number_of_rows, f):
    for i in range(number_of_rows):
        print(str(i+1) + '|' + get_lens_name() + '|' + get_mount() + '|' + get_purpose_lens()  + \
                '|' + get_diameter(), file=f)

# -------------------------------------------------------------------------------------------

def get_filter_name():
    name = ""
    name += str(chr(rd.randint(65, 90))) + str(chr(rd.randint(65, 90))) 
    flag = False
    if(rd.randint(0,1)):
        flag = True
        name += str(chr(rd.randint(65, 90)))
    if(rd.randint(0,1) and not flag):
        name += str(chr(rd.randint(48, 57)))

    return name

def get_purpose_filter():
    purpose = ""
    purpose += base_purpose_filter[rd.randint(0, len(base_purpose_filter)-1)]
    return purpose

def gen_filter(number_of_rows, f):
    for i in range(number_of_rows):
        print(str(i+1) + '|' + get_filter_name() + '|' + get_purpose_filter() + '|' +  get_diameter(),
                file=f)

# -------------------------------------------------------------------------------------------

def strTimeProp(start, end, format, prop):
    stime = time.mktime(time.strptime(start, format))
    etime = time.mktime(time.strptime(end, format))

    ptime = stime + prop * (etime - stime)

    return time.strftime(format, time.localtime(ptime))

def randomDate(start, end, prop):
    return strTimeProp(start, end, '%Y/%m/%d', prop)

def gen_build(number_of_rows, f):
    for i in range(number_of_rows):
        print(str(i+1) + '|' + str(rd.randint(0, 1000)) + '|' + str(rd.randint(0, 1000)) + '|' + \
                    str(rd.randint(0, 1000)) + '|' + str(rd.randint(351, 1410)) + '|' + \
                    randomDate("2008/1/1", "2018/09/01", rd.random()), file=f)

gen_cameraBody(1000, file_camBody)
gen_lens(1000, file_lens)
gen_filter(1000, file_filter)
gen_build(1000, file_camBuild)


file_lens.close()
file_filter.close() 
file_camBody.close()
file_camBuild.close()
