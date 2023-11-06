"""
Split tfm dataset into train/val

Example:

python split_dataset --in <path-to-dir-with-labels> --out <output-dir> --val 0.7 
"""


"""
Create YoloV5 type dataset from unity created dataset.

Example:
    python create_yolo_dataset.py
"""

import os
import argparse
import random
import shutil

from tqdm import tqdm

def parse_options():
    parser = argparse.ArgumentParser()
    parser.add_argument("--labels", default="tfm_dataset", help="input dataset annotations .txt files")
    parser.add_argument("--images", default="dataset", help="input dataset annotations .txt files")
    parser.add_argument("--output", default="tfm_dataset", help="output directory name")
    parser.add_argument("--val", type=float, default=0.3, help="validation percentage")
    return parser.parse_args()

def main(args):
    validation_percentage = args.val
    label_train_path = args.output + "/labels/train"
    label_val_path = args.output + "/labels/val"
    image_train_path = args.output + "/images/train"
    image_val_path = args.output + "/images/val"

    if not os.path.isdir(args.output):
        os.makedirs(args.output)
    
    for path in [label_train_path, label_val_path, image_train_path, image_val_path]:
        if not os.path.isdir(path):
            os.makedirs(path, exist_ok=True)

    label_filenames = [filename for filename in os.listdir(args.labels) if ".txt" in filename]
    for filename in tqdm(label_filenames):
        label_output_path = label_train_path
        image_output_path = image_train_path
        if random.random() < validation_percentage:
            label_output_path = label_val_path
            image_output_path = image_val_path
            
        # txt
        shutil.copy(args.labels + "/" + filename, label_output_path)

        # png
        image_name = filename.split(".")[0] + ".png"
        shutil.copy(args.images + "/" + image_name, image_output_path)

if __name__ == "__main__":
    args = parse_options()
    main(args)