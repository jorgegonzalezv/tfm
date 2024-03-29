"""
Transform seadronesee json labels to txt encoded labels for YoloV5 or TFMs

All seadronesee credit to: https://github.com/Ben93kie/SeaDronesSee/blob/main/utils/jsonTotxt.py
"""

#Converts a json annotation file to txt that works with darklabel
import argparse
import os
import json
import pandas as pd

# categories = {"0":"ignored", "1":"swimmer", "2":"boat","3":"jetski","4":"life_saving_appliances","5":"buoy"}
categories = [
    {"supercategory": "ignored", "id": 7, "name": "ignored region"}, 
    {"supercategory": "person", "id": 1, "name": "swimmer"}, 
    {"supercategory": "person", "id": 2, "name": "floater"}, 
    {"supercategory": "person", "id": 4, "name": "swimmer on boat"}, 
    {"supercategory": "person", "id": 5, "name": "floater on boat"}, 
    {"supercategory": "boat", "id": 3, "name": "boat"}, 
    {"supercategory": "lifejacket", "id": 6, "name": "life jacket"}
]

# traslate to yolo pretrained coco categories
seadronesee_to_coco = {
    1: 0,
    2: 0,
    3: 8,
    4: 0,
    5: 0,
    6: -1,
    7: -1
}

# traslate to yolo tfm categories
seadronesee_to_tfm = {
    1: 0,
    2: 0,
    3: 1,
    4: 0,
    5: 0,
    6: -1,
    7: -1
}

def main(opt):
    print(opt)
    with open(opt.json) as file:
        json_dict = json.load(file)

    if opt.dataset == "tfm":
        convert_dict = seadronesee_to_tfm
    else:
        convert_dict = seadronesee_to_coco

    if not os.path.exists(opt.output):
        os.makedirs(opt.output)

    annotations_df = pd.DataFrame(json_dict["annotations"])
    images_df = pd.DataFrame(json_dict["images"])
    for image_id, group_df in annotations_df.groupby("image_id"):
        image_data = images_df.loc[images_df.id == image_id]
        im_h = image_data["height"].item()
        im_w = image_data["width"].item()
        # output_path = opt.output + "/" + str(image_id).zfill(12) + ".txt"
        output_path = os.path.join(opt.output, str(image_id) + ".txt")

        with open(output_path, "w") as f:
            for row_idx, row in group_df.iterrows():
                label = int(row["category_id"])
                label = convert_dict[label]
                if label == -1:
                    continue
                w = row["bbox"][2]
                h = row["bbox"][3]
                xc = row["bbox"][0] + w/2
                yc = row["bbox"][1] + h/2
                xc = xc / im_w
                yc = yc / im_h
                w = w / im_w
                h = h / im_h
                lines = "{} {} {} {} {}\n".format(label, xc, yc, w, h)
                f.writelines(lines)

if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument("--json", "-j")
    parser.add_argument("--output", "-o")
    parser.add_argument("--dataset", "-d", default="tfm")
    opt = parser.parse_args()
    main(opt)