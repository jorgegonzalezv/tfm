"""
Create YoloV5 type dataset from UNITY created dataset. (TFM DATASET)

Example:
    python create_yolo_dataset.py
"""

import os
import pandas as pd
import matplotlib.pyplot as plt
import argparse
from tqdm import tqdm

def parse_options():
    parser = argparse.ArgumentParser()
    parser.add_argument("--input", default="dataset", help="input unity dataset directory")
    parser.add_argument("--output", default="tfm_dataset", help="output directory name")
    return parser.parse_args()

DIR = os.path.join("/Users","jorge", "prueba-crest", "dataset")
SUPERSIZE = 4  # screenshot.cs
LABELS = {
    "person": 0,
    "boat": 1
}

def get_line(row, im):
    im_h, im_w, _ = im.shape

    label = LABELS[row.values[-1]]
    xmax, ymax, xmin, ymin = row[:4] * SUPERSIZE
    ymax = im_h - ymax 
    ymin = im_h - ymin # true ymax

    # add bbox
    w = abs(xmax - xmin) / im_w
    h = abs(ymax - ymin) / im_h

    xc = (xmax + xmin) / 2 
    yc = (ymax + ymin) / 2

    xc = xc / im_w
    yc = yc / im_h

    # check if outside image
    if (xc + w/2 <= 0) or (yc + h/2 <= 0):
        return None

    if (xc - w/2 >= 1) or (yc - h/2 >= 1):
        return None

    return "{} {} {} {} {}\n".format(label, xc, yc, w, h)


def main(args):
    if not os.path.isdir(args.output):
        os.makedirs(args.output)
        print("Created: {} dir".format(args.output))

    df = pd.read_csv(os.path.join(args.input,"debug.txt"), header=None)
    df = df.set_index(0)
    for group_idx, group_df in tqdm(df.groupby(df.index)):
        im_path = os.path.join(DIR, "screenshot{}.png".format(int(group_idx)))
        im = plt.imread(im_path)
        with open(args.output + "/screenshot{}.txt".format(group_idx), "w") as f:
            for row_idx, row in group_df.iterrows():
                lines = get_line(row, im)
                if lines is not None:
                    f.writelines(lines)

if __name__ == "__main__":
    args = parse_options()
    main(args)